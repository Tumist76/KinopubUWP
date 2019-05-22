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

        #endregion

        #region Приватные поля

        /// <summary>
        /// Таск с информацией о коде устройства
        /// </summary>
        private NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> DeviceCodeRequestTask { get; set; }

        private NotifyTaskCompletion<IRestResponse<AccessTokenRequest>> AccessTokenRequestTask { get; set; }

        private DispatcherTimer countdownTimer;

        #endregion

        #region Методы

        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            DeviceCodeRequestTask = new NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>(Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret));
            //Подписываемся на обновление свойств внутри объекта с свойствами таска получения кода авторизации
            this.DeviceCodeRequestTask.PropertyChanged += AuthorizationViewModel_PropertyChanged;
        }

        public void GetAccessToken()
        {
            AccessTokenRequestTask = new NotifyTaskCompletion<IRestResponse<AccessTokenRequest>>(Auth.GetAccessTokenAsync(Constants.DeviceId, Constants.DeviceSecret, CodeRequest.code));
            this.AccessTokenRequestTask.PropertyChanged += AuthorizationViewModel_PropertyChanged;
            ExpirationCountdown();
        }

        public async Task StartTokenRequest()
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


        #endregion

        #region События

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Result":
                    if (DeviceCodeRequestTask.IsSuccessfullyCompleted) CodeRequest = DeviceCodeRequestTask.Result.Data;
                    GetAccessToken();
                    break;
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
        }

        #endregion
    }
}