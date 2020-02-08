using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem
{
    /// <summary>
    /// Класс для элементов треклиста 
    /// </summary>
    public class MusicTrack
    {
        /// <summary>
        /// Исполнитель
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Название композиции
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// ссылка на аудио файл
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}