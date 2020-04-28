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

        public WatchingEntity()
        {
        }

        private void SetItemName(string name)
        {
            if (String.IsNullOrEmpty(item.Title)) return;

            var titles = item.Title.Split('/');
            //Устанавливаем русское название
            russianItemTitle = titles[0].Trim();
            //Устанавливаем оригинальное название при наличии
            if (titles.Length == 2)
            {
                originalItemTitle = titles[1].Trim();
            }
        }
        //Оригинальное название тайтла
        public string OriginalItemTitle
        {
            get => originalItemTitle;
            set => originalItemTitle = value;
        }

        //Русское название тайтла
        public string RussianItemTitle
        {
            get => russianItemTitle;
            set => russianItemTitle = value;
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
        private string originalItemTitle;
        private string russianItemTitle;

        #endregion

    }
}
