using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class VideoItem
    {
        #region Общая информация

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")] // 'Название / Оригинальное название'
        public string Title { get; set; }

        /// <summary>
        /// фильтр по типу жанров, по умолчанию возвращаются все жанры. 
        /// </summary>
        /// <remarks>
        /// movie - Фильмы
        /// serial - Сериалы
        /// 3D - 3D Фильмы
        /// concert - Концерты
        /// documovie - Документальные фильмы
        /// docuserial - Документальные сериалы
        /// </remarks>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("cast")]
        public string Cast { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        /// <summary>
        /// Описания аудиодорожек
        /// "Профессиональный многоголосый, Профессиональный двухголосый, Авторский одноголосый, Оригинал"
        /// </summary>
        [JsonProperty("voice")]
        public string Voice { get; set; }

        /// <summary>
        /// //Количество аудио дорожек
        /// </summary>
        [JsonProperty("langs")]
        public int Languages { get; set; }

        /// <summary>
        /// Присутствуют или нет AC-3 аудио
        /// </summary>
        [JsonProperty("ac3")]
        public int Ac3 { get; set; }

        /// <summary>
        /// Количество субтитров
        /// </summary>
        [JsonProperty("subtitles")]
        public int Subtitles { get; set; }

        /// <summary>
        /// Качество фильма, для сериалов берется наибольшее количество серий с определенным качеством
        /// </summary>
        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("countries")]
        public List<Country> Countries { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("tracklist")]
        public List<MusicTrack> Tracklist { get; set; }

        /// <summary>
        /// Идентификатор тайтла на IMDb
        /// </summary>
        /// <remarks>
        /// у них он семизначный, добавлять нули в начало, если ID меньшей длины
        /// </remarks>
        [JsonProperty("imdb")]
        public string ImdbId { get; set; }

        [JsonProperty("imdb_rating")]
        public decimal ImdbRating { get; set; }

        [JsonProperty("imdb_votes")]
        public long ImdbVotes { get; set; }

        /// <summary>
        /// Идентификатор тайтла на Кинопоиске
        /// </summary>
        [JsonProperty("kinopoisk")]
        public string KinopoiskId { get; set; }
        [JsonProperty("kinopoisk_rating")]
        public decimal KinopoiskRating { get; set; }
        [JsonProperty("kinopoisk_votes")]
        public long KinopoiskVotes { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("rating_votes")]
        public int RatingVotes { get; set; }
        [JsonProperty("rating_percentage")]
        public decimal RatingPercentage { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        /// <summary>
        /// Количество комментов
        /// </summary>
        [JsonProperty("comments")]
        public long Comments { get; set; }

        /// <summary>
        /// Для сериалов: true - окончен, false - снимается
        /// </summary>
        [JsonProperty("finished")]
        public bool Finished { get; set; }

        /// <summary>
        /// Присутствуют посторонние вставки рекламы
        /// </summary>
        [JsonProperty("advert")]
        public bool Advert { get; set; }

        /// <summary>
        /// Подписан ли пользователь на сериал
        /// </summary>
        [JsonProperty("in_watchlist")]
        public bool InWatchlist { get; set; }

        /// <summary>
        /// Подписан ли пользователь на сериал, алиас для in_watchlist
        /// </summary>
        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("posters")]
        public Poster Posters { get; set; }

        [JsonProperty("trailer")]
        public Trailer Trailer { get; set; }
        
        #endregion

        #region Видеофайлы

        

        #endregion
    }
}