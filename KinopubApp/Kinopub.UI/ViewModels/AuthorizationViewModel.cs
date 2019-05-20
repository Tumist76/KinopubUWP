using System.ComponentModel;
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
            //UserCode = "*****";
            GetDeviceCode();
        }

        #endregion


        #region Публичные поля
        /// <summary>
        /// Таск с информацией о коде устройства
        /// </summary>
        public NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> DeviceCodeRequest {
            get => deviceCodeRequest;
                set
            {
                deviceCodeRequest = value;
                if (value.IsSuccessfullyCompleted) UserCode = value.Result.Data.user_code;
            }
        }

        public string UserCode {
            get;
            set;
        }
        #endregion

        private NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> deviceCodeRequest;
        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            DeviceCodeRequest = new NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>(Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret));
        }

    }
}