using Kinopub.Api.Entities.VideoContent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.Api.Entities
{
    class GetItem
    {
        [JsonProperty("item")]
        public ItemContent Item { get; set; }
    }
}
