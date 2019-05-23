using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.UI.Utilities;
using Kinopub.Api.Entities.Auth;
using RestSharp.Portable;
using System;
using Windows.UI.Xaml;

namespace Kinopub.UI.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Конструктор

        public AuthorizationViewModel()
        {
            CodeRequest = new DeviceCodeRequest();

            CodeRequest.user_code = "*****";

            GetDeviceCode();

        }

        #endregion

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

        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            //if (deviceCodeRequestTask != null) this.deviceCodeRequestTask.PropertyChanged -= AuthorizationViewModel_PropertyChanged;
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

        public async Task CheckRequestTask()
        {
            //TODO 
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
            WindowNavigation.WindowNavigateTo(typeof(MainPage), null);
        }
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
                if (!accessTokenRequestTask.IsSuccessfullyCompleted)
                {
                    return;
                }
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
            if (CountdownCounter % CodeRequest.interval == 0)
            {
                GetAccessToken();
                return;
            }
        }

        #endregion
    }
}