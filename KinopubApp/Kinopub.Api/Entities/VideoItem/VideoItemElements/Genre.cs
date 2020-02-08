using Newtonsoft.Json;

namespace Kinopub.Api.Entities.VideoContent.VideoItem
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// фильтр по типу жанров, по умолчанию возвращаются все жанры. 
        /// </summary>
        /// <remarks>
        /// Указать можно только один из нижеперечисленных
        /// movie - Обощенный тип
        /// docu - Обобщенный тип
        /// music - Обобщенный тип
        /// tvshow - Обобщенный тип
        /// movie
        /// documovie
        /// serial
        /// docuserial
        /// tvshow
        /// concert
        /// 3d
        /// 4k
        /// </remarks>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}