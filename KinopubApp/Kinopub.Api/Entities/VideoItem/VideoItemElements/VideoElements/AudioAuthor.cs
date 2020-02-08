using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem.VideoElements
{
    public class AudioAuthor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("short_title")]
        public string ShortTitle { get; set; }
    }
}