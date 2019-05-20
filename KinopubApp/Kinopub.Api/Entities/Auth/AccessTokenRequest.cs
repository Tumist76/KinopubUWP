using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.Api.Entities.Auth
{
    /// <summary>
    /// Поля запроса получения токена доступа
    /// </summary>
    class AccessTokenRequest
    {
        /// <summary>
        /// Токен для доступа к API
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// Тип токена (должен быть 'bearer')
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// Время до окончания действия токена (в минутах, как я понимаю)
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// Токен для запроса на обновление access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// Не используется
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// Описание ошибки получения токена
        /// </summary>
        /// <remarks>
        /// "authorization_pending" не является настоящей ошибкой, 
        /// а только свидетельсвует о том, что ожидается активация на сайте
        /// </remarks>
        public string error { get; set; }
    }
}
