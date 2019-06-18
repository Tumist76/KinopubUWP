using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities
{
    public class SearchEntity
    {
        [JsonProperty("items")]
        public List<VideoItem> Items { get; set; }

        [JsonProperty("pagination")]
        public List<Pagination> Pagination { get; set; }
    }
}