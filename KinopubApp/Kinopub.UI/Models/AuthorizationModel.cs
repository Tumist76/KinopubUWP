using Kinopub.Api;
using Kinopub.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kinopub.UI.Models
{
    /// <summary>
    /// Методы для проверки и получения токенов авторизации
    /// </summary>
    public class AuthorizationModel
    {
        /// <summary>
        /// Возвращает композитное значение сохраненных данных авторизации
        /// </summary>
        /// <returns></returns>
        public ApplicationDataCompositeValue GetAuthData()
        {
            return SettingsManager.GetLocalCompositeContainer(SettingsConstants.AuthData);
        }
        /// <summary>
        /// Проверяет наличие и валидность токена авторизации
        /// </summary>
        /// <returns></returns>
        public bool isAuthorized()
        {
            var authData = GetAuthData();
            if (IsAccessTokenPresent(authData))
            {
                if (IsAccessTokenValid(authData))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// Обновляет токен доступа
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAccessToken()
        {
            var refreshTokenTask = await Auth.RefreshTokenAsync(Constants.DeviceId, Constants.DeviceSecret, GetAuthData()[SettingsConstants.AuthRefreshToken].ToString());
            if (refreshTokenTask.IsSuccess)
            {
                SaveAuthData(
                    refreshTokenTask.Data.access_token,
                    refreshTokenTask.Data.refresh_token,
                    refreshTokenTask.Data.expires_in);
            }
        }
        /// <summary>
        /// Проверяет валидность токена доступа
        /// </summary>
        /// <param name="authData"></param>
        /// <returns></returns>
        public bool IsAccessTokenValid(ApplicationDataCompositeValue authData)
        {
            if (!IsAccessTokenPresent(authData))
            {
                return false;
            }
            if ((DateTimeOffset)authData[SettingsConstants.AuthAccessTokenExpiration] <= DateTimeOffset.Now)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверяет наличие сохранённого токена доступа
        /// </summary>
        /// <param name="authData"></param>
        /// <returns></returns>
        public bool IsAccessTokenPresent(ApplicationDataCompositeValue authData)
        {
            if (authData == null)
            {
                return false;
            }
            if (authData[SettingsConstants.AuthAccessToken] == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверяет валидность сохраненного токена обновления
        /// </summary>
        /// <param name="authData"></param>
        /// <returns></returns>
        public bool IsRefreshTokenValid(ApplicationDataCompositeValue authData)
        {
            if (!IsAccessTokenPresent(authData))
            {
                return false;
            }
            if ((DateTimeOffset)authData[SettingsConstants.AuthRefreshTokenExpiration] <= DateTimeOffset.Now)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Сохраняет данные авторизаии в настройки
        /// </summary>
        /// <param name="accessToken">Токен доступа</param>
        /// <param name="refreshToken">Токен обновления</param>
        /// <param name="secondsToExpiration">Время жизни токена доступа (в секундах)</param>
        public void SaveAuthData(
            string accessToken,
            string refreshToken,
            int secondsToExpiration)
        {
            var composite = new ApplicationDataCompositeValue();
            composite[SettingsConstants.AuthAccessToken] = accessToken;
            composite[SettingsConstants.AuthRefreshToken] = refreshToken;

            var accessTokenExpiration = DateTimeOffset.Now.AddSeconds(secondsToExpiration);
            composite[SettingsConstants.AuthAccessTokenExpiration] = accessTokenExpiration;

            var refreshTokenExpiration = DateTimeOffset.Now.AddMonths(1);
            composite[SettingsConstants.AuthRefreshTokenExpiration] = refreshTokenExpiration;

            SettingsManager.SetLocalCompositeContainer(SettingsConstants.AuthData, composite);
        }
    }
}
