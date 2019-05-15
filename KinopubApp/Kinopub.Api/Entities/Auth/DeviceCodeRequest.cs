using System;
using System.Collections.Generic;
using System.Text;

namespace KinopubApi.Entities.Auth
{
    /// <summary>
    /// Класс для десериализации запроса на код устройства
    /// </summary>
    public class DeviceCodeRequest
    {
        /// <summary>
        /// Код для дальнейшего получения access token'a
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Код, который нужно показать пользователю
        /// </summary>
        public string user_code { get; set; }
        /// <summary>
        /// URL, где нужно ввести пользовательский код
        /// </summary>
        public string verification_uri { get; set; }
        /// <summary>
        /// Через сколько данный device_code истекает, в секундах
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// Интервал в секундах, через который посылать запросы на проверку активации
        /// </summary>
        public int interval { get; set; }
    }
}
