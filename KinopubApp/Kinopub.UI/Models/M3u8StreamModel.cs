using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kinopub.UI.Entities.M3u8;
using M3u8Parser.Entity;

namespace Kinopub.UI.Models
{
    /// <summary>
    /// Модель,
    /// </summary>
    class M3u8StreamModel
    {
        public ObservableCollection<M3u8Stream> VideoStreams { get; set; }

        public M3u8StreamModel(Uri playlistUri)
        {
            ParsePlaylist(GetPlaylistContent(playlistUri).Result);
        }

        private async Task<string> GetPlaylistContent(Uri playlistUri)
        {
            var client = new WebClient();
            var content = client.DownloadString(playlistUri);
            return content;
        }

        private async void ParsePlaylist(string playlistContent)
        {
            var parser = new M3u8Parser.Parser();
            parser.Load(playlistContent);
            var parsedMedia = await parser.ParseMedia();
            VideoStreams = new ObservableCollection<M3u8Stream>();
            foreach (var item in parsedMedia.Where(x => x.Class == "EXT-X-STREAM-INF"))
            {
                VideoStreams.Add(new M3u8Stream()
                {
                    ProgramId = item.ProgramId,
                    Resolution = item.Resolution.Height,
                    Bandwidth = item.Bandwidth,
                    AudioTrack = new List<M3u8Audio>(parsedMedia
                        .Where(x => x.GroupId == item.Audio)
                        .Select(x => new M3u8Audio()
                        {
                            GroupId = x.GroupId,
                            IsDefaultTrack = x.Default,
                            Name = x.Name,
                            Uri = new Uri(KinopubApi.Settings.Constants.CdnDomain + x.Url)
                        })),
                    SubtitleTrack = new List<M3u8Subtitle>(parsedMedia
                        .Where(x => x.GroupId == item.Subtitles)
                        .Select(x => new M3u8Subtitle()
                        {
                            GroupId = x.GroupId,
                            Name = x.Name,
                            Uri = new Uri(KinopubApi.Settings.Constants.CdnDomain + x.Url)
                        })),
                    Uri = new Uri(KinopubApi.Settings.Constants.CdnDomain + item.Url)
                });
            }
        }
    }
}
