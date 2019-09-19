using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class Video
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
        
        /// <summary>
        /// Длина в секундах
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Cтатус просмотра эпизода: -1 не смотрели вообще, 0 - начали смотреть, 1 - просмотрели
        /// </summary>
        [JsonProperty("watched")]
        public int Watched { get; set; }

        /// <summary>
        /// Тайминг процесса просмотра
        /// </summary>
        [JsonProperty("watching")]
        public Watching Watching { get; set; }

        /// <summary>
        /// Номера аудио-дорожек
        /// </summary>
        [JsonProperty("tracks")]
        public string Tracks { get; set; }

        [JsonProperty("subtitles")]
        public List<Subtitle> Subtitles { get; set; }

        [JsonProperty("audios")]
        public List<Audio> Audios { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }
    }
}