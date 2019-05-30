using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kinopub.Api.Entities.Auth
{
    /// <summary>
    /// Класс для десериализации запроса на код устройства
    /// </summary>
    public class DeviceCodeRequest
    {
        /// <summary>
        /// Код для дальнейшего получения access token'a
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// Код, который нужно показать пользователю
        /// </summary>
        [JsonProperty("user_code")]
        public string UserCode { get; set; }
        /// <summary>
        /// URL, где нужно ввести пользовательский код
        /// </summary>
        [JsonProperty("verification_uri")]
        public string VerificationUri { get; set; }
        /// <summary>
        /// Через сколько данный device_code истекает, в секундах
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Интервал в секундах, через который посылать запросы на проверку активации
        /// </summary>
        [JsonProperty("interval")]
        public int Interval { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChanged { get; set; }
    }
}
