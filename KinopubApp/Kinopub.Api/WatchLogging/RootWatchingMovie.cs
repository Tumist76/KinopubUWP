using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class RootWatchingMovie
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("items")]
        public List<WatchingMovie> WatchingMovies { get; set; }

      
    }
}