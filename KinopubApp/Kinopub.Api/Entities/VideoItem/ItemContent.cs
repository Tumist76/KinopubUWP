using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kinopub.Api.Entities.VideoContent
{
    public class ItemContent: VideoItem
    {
        [JsonProperty("videos")]
        public List<Video> Videos { get; set; }
    }
}