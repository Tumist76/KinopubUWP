using Newtonsoft.Json;
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
    public class AccessTokenRequest
    {
        /// <summary>
        /// Токен для доступа к API
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// Тип токена (должен быть 'bearer')
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        /// <summary>
        /// Время до окончания действия токена (в минутах, как я понимаю)
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Токен для запроса на обновление access_token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// Не используется
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
