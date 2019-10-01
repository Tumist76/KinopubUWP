using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class Subsciption
    {
        [JsonProperty("active")]
        public bool Username { get; set; }

        [JsonProperty("reg_date")] // 'Название / Оригинальное название'
        public long RegistrationDate { get; set; }
    }
}