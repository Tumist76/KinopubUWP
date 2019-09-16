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
    /// Модель, скачивающая плейлист m3u8 и сериализующая в список стримов
    /// </summary>
    class M3u8StreamModel
    {
        public async static Task<List<M3u8Stream>> GetStreams(Uri playlistUri)
        {
            string playlistContent = await GetPlaylistContent(playlistUri);
            return ParsePlaylist(playlistContent);
        }
        
        /// <summary>
        /// Скачивает плейлист в виде строки
        /// </summary>
        /// <param name="playlistUri">Ссылка на плейлист</param>
        /// <returns></returns>
        private static async Task<string> GetPlaylistContent(Uri playlistUri)
        {
            var client = new WebClient();
            //@todo сделать асинхронное скачивание (почему-то не хочет работать)
            var content = await client.DownloadStringTaskAsync(playlistUri);
            return content;
        }

        /// <summary>
        /// Парсит и возвращает список стримов
        /// </summary>
        /// <param name="playlistContent"></param>
        /// <returns></returns>
        private static List<M3u8Stream> ParsePlaylist(string playlistContent)
        {
            var parser = new M3u8Parser.Parser();
            parser.Load(playlistContent);
            var parsedMedia = parser.ParseMedia().Result;
            var videoStreams = new List<M3u8Stream>();
            foreach (var item in parsedMedia.Where(x => x.Class == "EXT-X-STREAM-INF"))
            {
                videoStreams.Add(new M3u8Stream()
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
            return videoStreams;
        }
    }
}
