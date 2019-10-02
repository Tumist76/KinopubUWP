using Newtonsoft.Json;

namespace Kinopub.Api.Entities.UserItem
{
    public class Profile
    {

        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }
    }
}