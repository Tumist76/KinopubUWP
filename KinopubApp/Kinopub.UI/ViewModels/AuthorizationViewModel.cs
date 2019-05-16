using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.UI.Utilities;
using KinopubApi.Entities.Auth;
using Prism.Mvvm;
using RestSharp.Portable;

namespace Kinopub.UI.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        #region Конструктор

        public AuthorizationViewModel()
        {
            GetDeviceCode();
        }

        #endregion


        #region Публичные поля
        /// <summary>
        /// Таск с информацией о коде устройства
        /// </summary>
        public NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>> DeviceCodeRequest { get; set; }

        #endregion
        /// <summary>
        /// Вызывает таск получения кода устройства
        /// </summary>
        public void GetDeviceCode()
        {
            DeviceCodeRequest = new NotifyTaskCompletion<IRestResponse<DeviceCodeRequest>>(Auth.GetDeviceCodeAsync(Constants.DeviceId, Constants.DeviceSecret));
        }

    }
}