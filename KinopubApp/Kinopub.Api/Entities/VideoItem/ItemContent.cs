using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kinopub.Api.Entities.VideoContent
{
    public class ItemContent: VideoItem
    {
        [JsonProperty("videos")]
        public List<Video> Videos { get; set; }

        public List<Season> Seasons { get; set; } 
    }
}