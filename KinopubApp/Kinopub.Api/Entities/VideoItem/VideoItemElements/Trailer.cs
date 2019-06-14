using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    /// <summary>
    /// Ссылка на трейлер
    /// </summary>
    public class Trailer
    {
        /// <summary>
        /// Идентификатор видео на YouTube
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}