using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.UI.Models;

namespace Kinopub.UI.Entities
{
    /// <summary>
    /// Модель вывода недосмотренных фильмов/сериалов
    /// </summary>
    public class WatchingModel
    {
        public ObservableCollection<WatchingEntity> WatchingEntities { get; set; }

        public WatchingModel()
        {
            GetCurrentlyWatchingTitles();
        }

        private async void GetCurrentlyWatchingTitles()
        {
            var requestManager = new ManageWatching(AuthTokenManagementModel.GetAuthToken());

            var watchingMovies = await requestManager.GetWatchingMovies();
            var watchingSerials = await requestManager.GetWatchingSubscribedSerials();

            WatchingEntities = new ObservableCollection<WatchingEntity>();

            foreach (var item in watchingMovies)
            {
                var watchingItem = await requestManager.GetWatchingItem(item.Id);
                var entity = new WatchingEntity(watchingItem);

                WatchingVideo video = new WatchingVideo();;
                if (watchingItem.Videos != null)
                {
                    video = GetVideoToPlay(watchingItem.Videos);
                }

                if (watchingItem.Seasons != null)
                {
                    video = GetVideoToPlay(GetSeasonToPlay(watchingItem.Seasons).Episodes);
                }

                entity.Duration = video.Duration;
                entity.Status = video.Status;
                entity.LastPosition = video.Time;
                entity.EpisodeTitle = video.Title;
                entity.Episode = video.Number;
            }
        }

        private WatchingSeason GetSeasonToPlay(List<WatchingSeason> seasons)
        {
                var startedSeason = seasons.FirstOrDefault(x => x.WatchedEpisodes < x.Episodes.Count);
                var notStartedSeason = seasons.FirstOrDefault(x => x.WatchedEpisodes == 0);

                if (startedSeason != null)
                {
                    return startedSeason;
                }
                else if (notStartedSeason != null)
                {
                    return notStartedSeason;
                }
                return null;
        }

        private WatchingVideo GetVideoToPlay(List<WatchingVideo> videos)
        {
                var startedEpisode = videos.FirstOrDefault(x => x.Status == WatchingStatus.Watching);
                if (startedEpisode != null)
                {
                    return startedEpisode;
                }
                var notStartedEpisode = videos.FirstOrDefault(x => x.Status == WatchingStatus.NotWatched);
                if (notStartedEpisode != null)
                {
                    return notStartedEpisode;
                }

                return null;
        }
    }
}
