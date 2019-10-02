using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class UserInfo
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}