﻿using Kinopub.Api;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.UI.Entities;
using Kinopub.UI.Models;
using Kinopub.UI.Utilities;
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

        public NotifyTaskCompletion<ObservableCollection<WatchingEntity>> WatchingEntitiesTask { get; set; }

        public WatchingVM()
        {
            WatchingEntitiesTask = new NotifyTaskCompletion<ObservableCollection<WatchingEntity>>(GetCurrentlyWatchingTitles()); 
        }

        private async Task<ObservableCollection<WatchingEntity>> GetCurrentlyWatchingTitles()
        {
            var authToken = AuthTokenManagementModel.GetAuthToken();
            var watchingManager = new ManageWatching(authToken);
            var contentManager = new GetContent(authToken);

            //Получаем список недосмотренных сериалов и фильмов
            var watchingMovies = await watchingManager.GetWatchingMovies();
            var watchingSerials = await watchingManager.GetWatchingSubscribedSerials();

            var entities = new ObservableCollection<WatchingEntity>();

            //@todo Эти два итератора выполняют почти одно и то же. 
            //Перенести в один метод, но так, чтобы не было кучи лишних инициализаций объектов зря.
            foreach (var item in watchingMovies.WatchingMovies)
            {
                
                WatchingItem watchingItem = await watchingManager.GetWatchingItem(item.Id);
                var entity = MakeEntity(watchingItem);

                ItemContent titleItem = await contentManager.GetItem(entity.TitleId);
                entity.Thumbnail = "https://cdn.service-kp.com/poster/item/big/" + entity.TitleId + ".jpg";

                //Изначально я хотел сделать широкие превьюшки для начатых эпизодов
                //Но визуально это выглядело слишком неравномерно

                //if (entity.Status == WatchingStatus.Watching)
                //{
                //    entity.Thumbnail = titleItem.Videos.FirstOrDefault(x => x.Id == entity.VideoId).Thumbnail;
                //}
                //else
                //{
                //    entity.Thumbnail = titleItem.Posters.MediumUrl;
                //}


                entity.EpisodesLeft = watchingItem.Videos.Count(x => x.Status != WatchingStatus.Watched) - 1;
                entities.Add(entity);
            }

            foreach (var item in watchingSerials.WatchingSerials)
            {
                WatchingItem watchingItem = await watchingManager.GetWatchingItem(item.Id);
                var entity = MakeEntity(watchingItem);
                
                var titleItem = await contentManager.GetItem(entity.TitleId);
                entity.Thumbnail = "https://cdn.service-kp.com/poster/item/big/" + entity.TitleId + ".jpg";
                //if (entity.Status == WatchingStatus.Watching)
                //{
                //    entity.Thumbnail = titleItem.Seasons.
                //        FirstOrDefault(x => x.Number == entity.Season).Episodes.
                //        FirstOrDefault(x => x.Id == entity.VideoId).Thumbnail;
                //}
                //else
                //{
                //    entity.Thumbnail = titleItem.Posters.MediumUrl;
                //}

                foreach (var season in titleItem.Seasons)
                {
                    entity.EpisodesLeft = season.Episodes.Count(x => x.Watching.Status != WatchingStatus.Watched) - 1;
                }
                entities.Add(entity);
            }

            return entities;
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
            entity.SeasonCount = item.Seasons != null ? item.Seasons.Count : 0;
            if (item.Seasons != null)
            {
                foreach (var season in item.Seasons)
                {
                    entity.EpisodeCount += season.Episodes.Count;
                }
            }

            if (item.Videos != null)
            {
                entity.EpisodeCount = item.Videos.Count;
            }
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
