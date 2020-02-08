using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.VideoItem.TypesConstants;

namespace Kinopub.UI.Entities
{
    public class WatchingEntity: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WatchingEntity(WatchingItem item)
        {
            this.item = item;
        }

        //Оригинальное название тайтла
        public string OriginalItemTitle
        {
            get
            {
                if (String.IsNullOrEmpty(item.Title)) return null;
                var titles = item.Title.Split('/');
                if (titles.Length == 2)
                {
                    return titles[1].Trim();
                }
                return null;
            }
        }

        //Русское название тайтла
        public string RussianItemTitle
        {
            get
            {
                if (String.IsNullOrEmpty(item.Title)) return null;
                var titles = item.Title.Split('/');
                return titles[0].Trim();
            }
        }
        /// <summary>
        /// Название для эпизода/этого видео
        /// </summary>
        public string EpisodeTitle
        {
            get
            {
                if (String.IsNullOrEmpty(episodeTitle)) return null;
                var titles = episodeTitle.Split('/');
                return titles[0].Trim();
            }
            set { episodeTitle = value; }
        }

        /// <summary>
        /// ИД тайтла (для перехода на его страницу)
        /// </summary>
        public int TitleId => item.Id;

        /// <summary>
        /// ИД видео (для сопоставления с сущностью видео, полученного через GetContent)
        /// </summary>
        public uint VideoId { get; set; }

        public int EpisodesLeft { get; set; }

        public WatchingStatus Status { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan LastPosition { get; set; }

        public double CompletionPercent
        {
            get { return LastPosition * 100 / Duration; }
        }

        public int SeasonCount { get; set; }

        public int EpisodeCount { get; set; }

        public int Season { get; set; }

        public int Episode { get; set; }

        /// <summary>
        /// Ссылка на превью фильма/эпизода, если просмотр начат; ссылка на постер, если еще не начинали смотреть
        /// </summary>
        public string Thumbnail { get; set; }

        #region Приватные свойства

        private WatchingItem item;

        private string episodeTitle;

        #endregion

    }
}
