using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class RootWatchingSerial
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("items")]
        public List<WatchingSerial> WatchingSerials { get; set; }

      
    }
}