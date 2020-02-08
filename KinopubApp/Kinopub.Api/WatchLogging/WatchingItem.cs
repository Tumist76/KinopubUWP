using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Kinopub.Api.Entities.VideoContent.VideoItem.TypesConstants;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class WatchingItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("status")]
        public WatchingStatus Status { get; set; }

        [JsonProperty("seasons")]
        public List<WatchingSeason> Seasons { get; set; }

        [JsonProperty("videos")]
        public List<WatchingVideo> Videos { get; set; }

        /// <summary>
        /// Число просмотренных сезонов
        /// </summary>
        [JsonProperty("watched")]
        public int Watched { get; set; }
    }
}