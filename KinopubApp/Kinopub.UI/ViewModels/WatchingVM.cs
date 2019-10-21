using Kinopub.Api;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.UI.Entities;
using Kinopub.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.UI.ViewModels
{
    class WatchingVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<WatchingEntity> WatchingEntities { get; set; }

        public WatchingVM()
        {
            GetCurrentlyWatchingTitles();
        }

        private async void GetCurrentlyWatchingTitles()
        {
            var authToken = AuthTokenManagementModel.GetAuthToken();
            var watchingManager = new ManageWatching(authToken);
            var contentManager = new GetContent(authToken);

            //Получаем список недосмотренных сериалов и фильмов
            var watchingMovies = await watchingManager.GetWatchingMovies();
            var watchingSerials = await watchingManager.GetWatchingSubscribedSerials();

            WatchingEntities = new ObservableCollection<WatchingEntity>();

            //@todo Эти два итератора выполняют почти одно и то же. 
            //Перенести в один метод, но так, чтобы не было кучи лишних инициализаций объектов зря.
            foreach (var item in watchingMovies.WatchingMovies)
            {
                WatchingItem watchingItem = await watchingManager.GetWatchingItem(item.Id);
                var entity = MakeEntity(watchingItem);

                ItemContent titleItem = await contentManager.GetItem(entity.TitleId);
                if (entity.Status == WatchingStatus.Watching)
                {
                    entity.Thumbnail = titleItem.Videos.FirstOrDefault(x => x.Id == entity.VideoId).Thumbnail;
                }
                else
                {
                    entity.Thumbnail = titleItem.Posters.MediumUrl;
                }

                entity.EpisodesLeft = watchingItem.Videos.Count(x => x.Status != WatchingStatus.Watched) - 1;
                WatchingEntities.Add(entity);
            }

            foreach (var item in watchingSerials.WatchingSerials)
            {
                WatchingItem watchingItem = await watchingManager.GetWatchingItem(item.Id);
                var entity = MakeEntity(watchingItem);

                var titleItem = await contentManager.GetItem(entity.TitleId);
                if (entity.Status == WatchingStatus.Watching)
                {
                    entity.Thumbnail = titleItem.Seasons.
                        FirstOrDefault(x => x.Number == entity.Season).Episodes.
                        FirstOrDefault(x => x.Id == entity.VideoId).Thumbnail;
                }
                else
                {
                    entity.Thumbnail = titleItem.Posters.MediumUrl;
                }
                
                foreach (var season in titleItem.Seasons)
                {
                    entity.EpisodesLeft = season.Episodes.Count(x => x.Watching.Status != WatchingStatus.Watched) - 1;
                }
                WatchingEntities.Add(entity);
            }
        }

        private WatchingEntity MakeEntity(WatchingItem item)
        {
            var entity = new WatchingEntity(item);
            WatchingVideo video = new WatchingVideo();

            WatchingSeason currentSeason = null;
            if (item.Videos != null)
            {
                video = GetVideoToPlay(item.Videos);
            }

            if (item.Seasons != null)
            {
                currentSeason = GetSeasonToPlay(item.Seasons);
                video = GetVideoToPlay(currentSeason.Episodes);
            }

            entity.Duration = TimeSpan.FromSeconds(video.Duration);
            entity.Status = video.Status;
            entity.LastPosition = TimeSpan.FromSeconds(video.Time);

            //@todo глянуть, можно ли упростить проверку "сериальности"
            if (currentSeason != null)
            {
                entity.Season = currentSeason.Number;
            }
            entity.EpisodeTitle = video.Title;
            entity.Episode = video.Number;

            entity.VideoId = video.Id;

            return entity;
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
