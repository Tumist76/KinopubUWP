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
using Windows.Storage;
using Kinopub.UI.Models;

namespace Kinopub.UI.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AuthorizationModel authModel;

        #region Конструктор

        public AuthorizationViewModel()
        {
            authModel = new AuthorizationModel();

            CodeRequest = new DeviceCodeRequest();

            CodeRequest.user_code = "*****";
            authModel.CodeRequest.PropertyChanged += AuthorizationViewModel_PropertyChanged;
            authModel.GetDeviceCode();
            
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
        public int CountdownCounter
        {
            get => authModel.CountdownCounter;
        }

        #endregion

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender.GetType() == typeof(DeviceCodeRequest))
            {
                CodeRequest = (DeviceCodeRequest)sender;
            }
        }
    }
}