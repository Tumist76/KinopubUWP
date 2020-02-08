using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem
{
    /// <summary>
    /// Продолжительность видео
    /// </summary>
    public class Duration
    {
        /// <summary>
        /// Средняя продолжительность для сериалов, полная для фильмов (в секундах)
        /// </summary>
        [JsonProperty("average")]
        public int Average { get; set; }

        /// <summary>
        /// /Общая продолжительность фильма, сериала (в секундах)
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}