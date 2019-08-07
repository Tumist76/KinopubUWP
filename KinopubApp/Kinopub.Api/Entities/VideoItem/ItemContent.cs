using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kinopub.Api.Entities.VideoContent
{
    public class ItemContent: VideoItem
    {
        [JsonProperty("videos")]
        public List<Video> Videos { get; set; }

        //TODO Я не знаю, что ты за фигню наворотил, но сделай нормально
        public string Hls4Url
        {
            get
            {
                if (Videos.Capacity > 0)
                {
                    if (Videos.First().Files.First().Url.Hls4Url != null)
                        return Videos.First().Files.First().Url.Hls4Url;
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}