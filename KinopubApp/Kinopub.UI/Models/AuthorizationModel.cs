﻿using Kinopub.Api;
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
    public class AuthorizationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AuthorizationModel()
        {
            DeviceCodeRequest = null;
        }

        #region Публичные поля

        /// <summary>
        /// Обратный счётчик до окончания действия кода авторизации
        /// </summary>
        public int CountdownCounter { get; set; }
        /// <summary>
        /// Запрос на код устройства
        /// </summary>
        public IRestResponse<DeviceCodeRequest> DeviceCodeRequest { get; set; }

        public bool Authorized { get; set; }

        #endregion


        #region Приватные поля

        private DispatcherTimer countdownTimer;

        #endregion


        #region Методы

        #region Получение токена авторизации

        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public async Task GetDeviceCodeAsync()
        {
            DeviceCodeRequest = await Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret);
            if (DeviceCodeRequest.IsSuccess)
            {
                ExpirationCountdown();
            }
        }

        /// <summary>
        /// Выполняет запрос к серверу на получение токена авторизации и сохраняет при успехе
        /// </summary>
        /// <returns></returns>
        public async Task GetAccessTokenAsync()
        {
            var tokenRequest = await Auth.GetAccessTokenAsync(Constants.DeviceId, Constants.DeviceSecret, DeviceCodeRequest.Data.Code);

            if (tokenRequest != null)
            {
                countdownTimer.Stop();
                AuthTokenManagementModel.SaveAuthData
                (tokenRequest.AccessToken,
                    tokenRequest.RefreshToken,
                    tokenRequest.ExpiresIn
                );
                Authorized = true;
            }
        }

        /// <summary>
        /// Запускает таймер обратного отсчёта до прекращения действия кода авторизации
        /// </summary>
        private void ExpirationCountdown()
        {
            CountdownCounter = DeviceCodeRequest.Data.ExpiresIn;
            countdownTimer = new DispatcherTimer();
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Interval = new TimeSpan(0, 0, 1);
            countdownTimer.Start();
        }
        
        #endregion


        #endregion

        #region События

        private void CountdownTimer_Tick(object sender, object e)
        {
            CountdownCounter--;
            //Обновляет код по окончанию его действия
            if (CountdownCounter <= 0)
            {
                countdownTimer.Stop();
                GetDeviceCodeAsync();
            }
            //Выполняет запрос на получение токена доступа через заданный API интервал
            if (CountdownCounter % DeviceCodeRequest.Data.Interval == 0)
            {
                GetAccessTokenAsync();
                return;
            }
        }

        #endregion
    }
}
