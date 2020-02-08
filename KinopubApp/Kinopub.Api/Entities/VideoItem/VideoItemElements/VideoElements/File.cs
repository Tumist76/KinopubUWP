using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem.VideoElements
{
    public class File
    {
        /// <summary>
        /// Тип кодека
        /// </summary>
        [JsonProperty("codec")]
        public string Codec { get; set; }

        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        /// <summary>
        /// Порядковый номер потока
        /// </summary>
        [JsonProperty("quality_id")]
        public int QualityId { get; set; }

        [JsonProperty("url")]
        public FileUrl Url { get; set; }
    }
}