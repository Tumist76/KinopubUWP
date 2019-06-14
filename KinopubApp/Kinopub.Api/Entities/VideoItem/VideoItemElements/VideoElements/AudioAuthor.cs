using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoElements
{
    public class AudioAuthor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public int Title { get; set; }

        [JsonProperty("short_title")]
        public int ShortTitle { get; set; }
    }
}