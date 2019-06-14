using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoElements
{
    public class FileUrl
    {
        [JsonProperty("http")]
        public string HttpUrl { get; set; }
        [JsonProperty("hls")]
        public string HlsUrl { get; set; }
        [JsonProperty("hls4")]
        public string Hls4Url { get; set; }
        [JsonProperty("hls2")]
        public string Hls2Url { get; set; }
    }
}