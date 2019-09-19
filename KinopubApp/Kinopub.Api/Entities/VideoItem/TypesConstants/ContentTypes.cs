using System.Reflection.Metadata.Ecma335;
using Windows.Devices.Bluetooth.Advertisement;

namespace Kinopub.Api.Entities.VideoContent.TypesConstants
{
    /// <summary>
    /// Типы видеоконтента
    /// </summary>
    public class ContentTypesConverter
    {
        public static string GetContentType(ContentTypeEnum contentType)
        {
            switch (contentType)
            {
                case ContentTypeEnum.Movie:
                    return Movie;
                case ContentTypeEnum.Serial:
                    return Serial;
                case ContentTypeEnum.TvShow:
                    return TvShow;
                case ContentTypeEnum.Movie3d:
                    return Movie3d;
                case ContentTypeEnum.Concert:
                    return Concert;
                case ContentTypeEnum.MovieDocumental:
                    return MovieDocumental;
                case ContentTypeEnum.TvShowDocumental:
                    return TvShowDocumental;
                default: return null;
            }
        }
        /// <summary>
        /// Фильм
        /// </summary>
        public const string Movie = "movie";
        /// <summary>
        /// Сериал
        /// </summary>
        public const string Serial = "serial";
        /// <summary>
        /// Сериал
        /// </summary>
        public const string TvShow = "tvshow";
        /// <summary>
        /// 3D-фильм
        /// </summary>
        public const string Movie3d = "3D";
        /// <summary>
        /// Концерт
        /// </summary>
        public const string Concert = "concert";
        /// <summary>
        /// Документальный фильм
        /// </summary>
        public const string MovieDocumental = "documovie";
        /// <summary>
        /// Документальный сериал
        /// </summary>
        public const string TvShowDocumental = "docuserial";
    }
}