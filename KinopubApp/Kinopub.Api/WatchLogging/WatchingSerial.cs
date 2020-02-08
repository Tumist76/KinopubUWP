using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Kinopub.Api.Entities.VideoContent.VideoItem;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class WatchingSerial
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("posters")]
        public Poster Poster { get; set; }

        /// <summary>
        /// Общее число эпизодов
        /// </summary>
        [JsonProperty("total")]
        public int TotalEpisodes { get; set; }

        /// <summary>
        /// Число просмотренных сезонов
        /// </summary>
        [JsonProperty("watched")]
        public int Watched { get; set; }

        /// <summary>
        /// Число новых/недосмотренных эпизодов
        /// </summary>
        [JsonProperty("new")]
        public int NewEpisodes { get; set; }
    }
}