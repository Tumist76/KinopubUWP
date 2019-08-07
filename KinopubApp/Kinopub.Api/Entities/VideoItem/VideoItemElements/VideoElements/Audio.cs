using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoElements
{
    public class Audio
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Кодек (AAC, AC3)
        /// </summary>
        [JsonProperty("codec")]
        public string Codec { get; set; }

        /// <summary>
        /// Количество каналов
        /// </summary>
        [JsonProperty("channels")]
        public int Channels { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("type")]
        public AudioType Type { get; set; }

        [JsonProperty("author")]
        public AudioAuthor Author { get; set; }

    }
}