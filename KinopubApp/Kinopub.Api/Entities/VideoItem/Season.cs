using System.Collections.Generic;
using Kinopub.Api.Entities.VideoContent.VideoElements;
using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem
{
    public class Season
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("number")]
        public uint Number { get; set; }

        public Watching Watching { get; set; }

        public List<Video> Episodes { get; set; }

    }
}