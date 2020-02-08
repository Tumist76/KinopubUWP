using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem
{
    /// <summary>
    /// Ссылки на постеры разных размеров
    /// </summary>
    public class Poster
    {
        [JsonProperty("small")]
        public string SmallUrl { get; set; }

        [JsonProperty("medium")]
        public string MediumUrl { get; set; }

        [JsonProperty("big")]
        public string BigUrl { get; set; }

        [JsonProperty("wide")]
        public string WideUrl { get; set; }
    }
}