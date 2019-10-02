using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class UserInfo
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("reg_date")] 
        public long RegistrationDate { get; set; }

        [JsonProperty("subscription")]
        public Subsciption Subscription { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }
}