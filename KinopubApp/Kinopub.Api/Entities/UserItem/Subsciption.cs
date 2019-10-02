using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class Subsciption
    {
        /// <summary>
        /// Активна ли подписка
        /// </summary>
        [JsonProperty("active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Время окончания подписки
        /// </summary>
        [JsonProperty("end_time")] 
        public long EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("days")]
        public decimal DaysLeft { get; set; }
    }
}