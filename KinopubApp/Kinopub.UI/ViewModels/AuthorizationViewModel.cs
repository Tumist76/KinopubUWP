using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.UI.Utilities;
using KinopubApi.Entities.Auth;
using RestSharp.Portable;

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
            //Подписываемся на обновление свойств внутри объекта с свойствами таска получения кода авторизации
            this.DeviceCodeRequestTask.PropertyChanged += AuthorizationViewModel_PropertyChanged;
        }

        #endregion

        #region Публичные поля

        /// <summary>
        /// Результат запроса на код авторизации
        /// </summary>
        public DeviceCodeRequest CodeRequest { get; set; }

        #endregion

        #region Приватные поля

        /// <summary>
        /// Таск с информацией о коде устройства
        /// </summary>
        private NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> DeviceCodeRequestTask { get; set; }

        #endregion

        #region Методы
        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            DeviceCodeRequestTask = new NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>(Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret));
        }

        public void 
        #endregion

        #region События

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Result":
                    if (DeviceCodeRequestTask.IsSuccessfullyCompleted) CodeRequest = DeviceCodeRequestTask.Result.Data;
                    break;
            }
        }
        #endregion
    }
}