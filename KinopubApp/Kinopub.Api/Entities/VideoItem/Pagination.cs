using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent
{
    public class Pagination
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("current")]
        public string Current { get; set; }

        /// <summary>
        /// Количество элементов на страницу
        /// </summary>
        [JsonProperty("perpage")]
        public string PerPage { get; set; }
    }
}