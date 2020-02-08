using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Kinopub.Api.Entities.VideoContent.VideoItem;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class WatchingMovie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("posters")]
        public Poster Poster { get; set; }

    }
}