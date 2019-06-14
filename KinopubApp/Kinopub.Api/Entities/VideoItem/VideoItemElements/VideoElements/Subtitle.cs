using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoElements
{
    public class Subtitle
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Смещение относительно видео-потока
        /// </summary>
        [JsonProperty("shift")]
        public int Shift { get; set; }

        /// <summary>
        /// Доступно в файле-исходнике, вшиты в него отдельным стримом
        /// </summary>
        [JsonProperty("embed")]
        public bool Embed { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}