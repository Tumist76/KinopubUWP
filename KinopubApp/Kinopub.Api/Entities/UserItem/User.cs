using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("reg_date")] // 'Название / Оригинальное название'
        public long RegistrationDate { get; set; }
    }
}