using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class WatchingSeason
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public uint Number { get; set; }

        [JsonProperty("watched")]
        public int WatchedEpisodes { get; set; }

        [JsonProperty("episodes")]
        public List<WatchingVideo> Episodes { get; set; }

    }
}