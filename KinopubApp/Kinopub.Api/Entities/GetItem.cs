using Kinopub.Api.Entities.VideoContent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api.Entities.VideoContent.VideoItem;

namespace Kinopub.Api.Entities
{
    public class GetItem
    {
        [JsonProperty("item")]
        public ItemContent Item { get; set; }
    }
}
