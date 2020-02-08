using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class RootWatchingItem
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("item")]
        public WatchingItem WatchingItem { get; set; }

      
    }
}