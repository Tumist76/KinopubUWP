using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Kinopub.Api.Entities.VideoContent.VideoItem.TypesConstants;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class WatchingVideo
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Длительность в секундах
        /// </summary>
        [JsonProperty("duration")]
        public uint Duration { get; set; }

        /// <summary>
        /// Секунда, на которой остановлен просмотр
        /// </summary>
        [JsonProperty("time")]
        public uint Time { get; set; }

        [JsonProperty("status")]
        public WatchingStatus Status { get; set; }

        /// <summary>
        /// Время, когда последний раз просматривали видео
        /// </summary>
        [JsonProperty("updated")]
        public uint updated { get; set; }
    }
}