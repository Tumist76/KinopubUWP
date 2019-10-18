using System;
using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    /// <summary>
    /// Результат запроса на добавление/удаление из списка "Буду смотреть"
    /// </summary>
    public class WatchlistToggleResult
    {
        /// <summary>
        /// true - отмечено как "буду смотреть"
        /// </summary>
        [JsonProperty("watching")]
        public bool Watching { get; set; }
    }
}