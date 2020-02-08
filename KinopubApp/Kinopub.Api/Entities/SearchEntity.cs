using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.VideoItem;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities
{
    public class SearchEntity
    {
        [JsonProperty("items")]
        public List<VideoItem> Items { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}