using Kinopub.Api;
using Kinopub.Api.Entities.Auth;
using Kinopub.UI.Utilities;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Kinopub.UI.Models
{
    /// <summary>
    /// Методы для проверки и получения токенов авторизации
    /// </summary>
    public class AuthorizationModel
    {
        public AuthorizationModel()
        {
            CodeRequest = new DeviceCodeRequest();
        }

        #region Публичные поля

        /// <summary>
        /// Результат запроса на код авторизации
        /// </summary>
        public DeviceCodeRequest CodeRequest { get; set; }

        /// <summary>
        /// Обратный счётчик до окончания действия кода авторизации
        /// </summary>
        public int CountdownCounter { get; set; }

        public NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> DeviceCodeRequestTask { get; set; }

        public NotifyTaskCompletion<IRestResponse<AccessTokenRequest>> AccessTokenRequestTask { get; set; }

        #endregion

        #region Приватные поля

        /// <summary>
        /// Таск с информацией о коде устройства
        /// </summary>
        public NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> deviceCodeRequestTask;

        public NotifyTaskCompletion<IRestResponse<AccessTokenRequest>> accessTokenRequestTask;

        private DispatcherTimer countdownTimer;

        #endregion


        #region Методы

        #region Авторизация

        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            if (deviceCodeRequestTask != null) this.deviceCodeRequestTask.PropertyChanged -= AuthorizationViewModel_PropertyChanged;
            deviceCodeRequestTask = new NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>(Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret));
            //Подписываемся на обновление свойств внутри объекта с свойствами таска получения кода авторизации
            this.deviceCodeRequestTask.PropertyChanged += AuthorizationViewModel_PropertyChanged;
        }

        public void GetAccessToken()
        {
            if (accessTokenRequestTask != null) this.accessTokenRequestTask.PropertyChanged -= AuthorizationViewModel_PropertyChanged;
            accessTokenRequestTask = new NotifyTaskCompletion<IRestResponse<AccessTokenRequest>>(Auth.GetAccessTokenAsync(Constants.DeviceId, Constants.DeviceSecret, CodeRequest.code));
            this.accessTokenRequestTask.PropertyChanged += AuthorizationViewModel_PropertyChanged;
        }

        /// <summary>
        /// Запускает таймер обратного отсчёта до прекращения действия кода авторизации
        /// </summary>
        private void ExpirationCountdown()
        {
            CountdownCounter = CodeRequest.expires_in;
            countdownTimer = new DispatcherTimer();
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Interval = new TimeSpan(0, 0, 1);
            countdownTimer.Start();
        }
        /// <summary>
        /// Сохраняет данные авторизации и перенаправляет на главную страницу
        /// </summary>
        private void FinishAuthoriztion()
        {
            countdownTimer.Stop();
            var authModel = new AuthorizationModel();
            authModel.SaveAuthData
                (AccessTokenRequestTask.Result.Data.access_token,
                AccessTokenRequestTask.Result.Data.refresh_token,
                AccessTokenRequestTask.Result.Data.expires_in
                );
            WindowNavigation.WindowNavigateTo(typeof(MainPage), null);
        }

        #endregion

        #region Работа с токенами и локальным хранилищем

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

        #endregion

        #endregion

        #region События

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender.GetType() == typeof(NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>))
            {
                DeviceCodeRequestTask = sender as NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>;
                switch (e.PropertyName)
                {
                    case "Result":
                        if (deviceCodeRequestTask.IsSuccessfullyCompleted) CodeRequest = deviceCodeRequestTask.Result.Data;
                        ExpirationCountdown();
                        break;
                }
            }
            if (sender.GetType() == typeof(NotifyTaskCompletion<IRestResponse<AccessTokenRequest>>))
            {
                AccessTokenRequestTask = sender as NotifyTaskCompletion<IRestResponse<AccessTokenRequest>>;
                switch (e.PropertyName)
                {
                    case "Result":
                        if (!String.IsNullOrEmpty(accessTokenRequestTask.Result.Data.access_token))
                        {
                            FinishAuthoriztion();
                            break;
                        }
                        break;
                }
            }
        }

        private void CountdownTimer_Tick(object sender, object e)
        {
            CountdownCounter--;
            //Обновляет код по окончанию его действия
            if (CountdownCounter <= 0)
            {
                countdownTimer.Stop();
                GetDeviceCode();
            }
            //Выполняет запрос на получение токена доступа через заданный API интервал
            if (CountdownCounter % CodeRequest.interval == 0)
            {
                GetAccessToken();
                return;
            }
        }

        #endregion
    }
}
