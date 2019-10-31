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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kinopub.UI.ViewModels
{
    class WatchingVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyTaskCompletion<ObservableCollection<WatchingEntity>> WatchingEntitiesTask { get; set; }

        public string WatchingBlockBackgroundImage { get; set; }


        public WatchingVM()
        {
            WatchingEntitiesTask = new NotifyTaskCompletion<ObservableCollection<WatchingEntity>>(GetCurrentlyWatchingTitles());
        }

        private string authToken;
        private async Task<ObservableCollection<WatchingEntity>> GetCurrentlyWatchingTitles()
        {
            authToken = AuthTokenManagementModel.GetAuthToken();
            var watchingManager = new ManageWatching(authToken);
            var contentManager = new GetContent(authToken);

            //Получаем список недосмотренных сериалов и фильмов
            var watchingMovies = await watchingManager.GetWatchingMovies();
            var watchingSerials = await watchingManager.GetWatchingSubscribedSerials();

            var entities = new List<WatchingEntity>();

            var tasksList = new List<Task<WatchingEntity>>();
            foreach (var item in watchingMovies.WatchingMovies)
            {
                tasksList.Add(Task.Run(() => MakeEntity(item.Id)));
            }

            foreach (var item in watchingSerials.WatchingSerials)
            {
                tasksList.Add(Task.Run(() => MakeEntity(item.Id)));
            }
            //@todo ловить исключения
            await Task.WhenAll(tasksList.ToArray());
            foreach (Task<WatchingEntity> task in tasksList.ToArray())
            {
                if (task.IsCompletedSuccessfully == true)
                    entities.Add(task.Result);
            }

            entities = entities.OrderByDescending(x => x.CompletionPercent).ToList();
            return new ObservableCollection<WatchingEntity>(entities);
        }

        private WatchingEntity MakeEntity(int titleId)
        {
            Debug.WriteLine("Thread {0} - Start {1}", Thread.CurrentThread.ManagedThreadId, titleId);
            //@todo не уверен, что создавать столько экземпляров объекта нормально.
            //Может, стоит передавать объекты как параметры в метод?
            var watchingManager = new ManageWatching(authToken);
            var contentManager = new GetContent(authToken);

            WatchingItem watchingItem = watchingManager.GetWatchingItem(titleId).Result;
            Debug.WriteLine("Title {0}", watchingItem.Title);

            var entity = new WatchingEntity(watchingItem);
            WatchingVideo video = new WatchingVideo();

            WatchingSeason currentSeason = null;
            if (watchingItem.Videos != null)
            {
                video = GetVideoToPlay(watchingItem.Videos);
            }

            if (watchingItem.Seasons != null)
            {
                currentSeason = GetSeasonToPlay(watchingItem.Seasons);
                video = GetVideoToPlay(currentSeason.Episodes);
            }

            entity.Duration = TimeSpan.FromSeconds(video.Duration);
            entity.Status = video.Status;
            entity.LastPosition = TimeSpan.FromSeconds(video.Time);
            entity.SeasonCount = watchingItem.Seasons != null ? watchingItem.Seasons.Count : 0;
            if (watchingItem.Seasons != null)
            {
                foreach (var season in watchingItem.Seasons)
                {
                    entity.EpisodeCount += season.Episodes.Count;
                }
            }

            if (watchingItem.Videos != null)
            {
                entity.EpisodeCount = watchingItem.Videos.Count;
            }
            //@todo глянуть, можно ли упростить проверку "сериальности"
            if (currentSeason != null)
            {
                entity.Season = currentSeason.Number;
            }
            entity.EpisodeTitle = video.Title;
            entity.Episode = video.Number;

            entity.VideoId = video.Id;

            var titleItem = contentManager.GetItem(entity.TitleId);
            entity.Thumbnail = "https://cdn.service-kp.com/poster/item/big/" + entity.TitleId + ".jpg";

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
