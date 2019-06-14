using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}