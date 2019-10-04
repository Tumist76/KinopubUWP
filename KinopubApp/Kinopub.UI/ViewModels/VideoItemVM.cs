using Kinopub.Api.Entities.VideoContent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;
using Kinopub.Api;
using Kinopub.UI.Models;
using System.ComponentModel;

namespace Kinopub.UI.ViewModels
{
    class VideoItemVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ItemContent ItemProperties
        {
            get => itemProperties;
            set
            {
                itemProperties = value;
                ManageContent();
            }
        }

        #region Методы для определения серии и сезона на воспроизведение
        
        private void ManageContent()
        {
            if (itemProperties.Seasons != null)
            {
                var startedSeason = itemProperties.Seasons.FirstOrDefault(x => x.Watching.Status == 0);
                if (startedSeason != null)
                {
                    SeasonToPlay = startedSeason;
                    return;
                }
                var notStartedSeason = itemProperties.Seasons.FirstOrDefault(x => x.Watching.Status == -1);
                if (notStartedSeason != null)
                {
                    SeasonToPlay = notStartedSeason;
                    return;
                }
                SeasonToPlay = itemProperties.Seasons.LastOrDefault(x => x.Watching.Status == 1);
            }
        }

        private void GetVideoToPlay(List<Video> videos)
        {
            if (itemProperties.Seasons != null)
            {
                var startedEpisode = VideoList.FirstOrDefault(x => x.Watching.Status == 0);
                if (startedEpisode != null)
                {
                    VideoToPlay = startedEpisode;
                    return;
                }
                var notStartedEpisode = VideoList.FirstOrDefault(x => x.Watching.Status == -1);
                if (notStartedEpisode != null)
                {
                    VideoToPlay = notStartedEpisode;
                    return;
                }
                VideoToPlay = VideoList.LastOrDefault(x => x.Watching.Status == 1);
            }

            VideoToPlay = VideoList.FirstOrDefault();
        }

        #endregion

        /// <summary>
        /// Идентификатор фильма/сериала
        /// </summary>
        public long ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                ItemProperties = new GetContent(AuthTokenManagementModel.GetAuthToken()).GetItem(itemId).Result;
            }
        }

        private long itemId;

        /// <summary>
        /// Объект со свойствами фильма/сериала
        /// </summary>
        private ItemContent itemProperties;

        /// <summary>
        /// Список видео, доступных для воспроизведения
        /// </summary>
        public ObservableCollection<Video> VideoList { get; set; }

        public Season SeasonToPlay
        {
            get { return seasonToPlay; }
            set
            {
                //@todo привязка к выбранному сезону в ComboBox
                seasonToPlay = value;
                VideoList = new ObservableCollection<Video>(seasonToPlay.Episodes);
                GetVideoToPlay(seasonToPlay.Episodes);
            }
        }

        private Season seasonToPlay;

        public Video VideoToPlay { get; set; }

        public int DurationInMinutes
        {
            get
            {
                if (ItemProperties.Videos != null)
                    return itemProperties.Videos.First().Duration / 60;
                else return 0;
            }
        }

        public VideoItemVM()
        {
            
        }
    }
}
