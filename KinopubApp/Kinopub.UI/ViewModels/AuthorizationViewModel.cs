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

        private AuthorizationModel authorizationModel;

        #region Конструктор

        public AuthorizationViewModel()
        {
            authorizationModel = new AuthorizationModel();
            authorizationModel.PropertyChanged += AuthorizationViewModel_PropertyChanged;
            authorizationModel.GetDeviceCodeAsync();
        }

        #endregion


        #region Публичные поля

        public IRestResponse<DeviceCodeRequest> DeviceCodeRequest { get; set; }

        public int CountdownCounter { get; set; }

        #endregion


        #region События

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DeviceCodeRequest":
                    DeviceCodeRequest = ((AuthorizationModel)sender).DeviceCodeRequest;
                    break;
                case "CountdownCounter":
                    CountdownCounter = ((AuthorizationModel)sender).CountdownCounter;
                    break;
                case "Authorized":
                    WindowNavigation.WindowNavigateTo(typeof(MainPage), null);
                    break;
            }
        }

        #endregion

    }
}