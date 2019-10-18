using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoElements
{
    public class Watching
    {
        /// <summary>
        /// Статус просмотра эпизода: -1 не смотрели вообще, 0 - начали смотреть, 1 - просмотрели
        /// </summary>
        [JsonProperty("status")]
        public WatchingStatus Status { get; set; }
        /// <summary>
        /// Время просмотра в секундах
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}