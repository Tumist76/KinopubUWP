using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.UI.Utilities;
using Kinopub.Api.Entities.Auth;
using RestSharp.Portable;
using System;
using System.Net;
using Windows.UI.Xaml;
using Windows.Storage;
using Kinopub.UI.Models;
using Windows.UI.Popups;
using MvvmDialogs;
using Windows.UI.Xaml.Controls;
using Kinopub.UI.Views;

namespace Kinopub.UI.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        #region Приватные поля

        private AuthorizationModel authorizationModel;

        private RelayCommand refreshCodeCommand;

        #endregion


        #region Конструктор

        public AuthorizationViewModel()
        {

            authorizationModel = new AuthorizationModel();
            authorizationModel.PropertyChanged += AuthorizationViewModel_PropertyChanged;
            authorizationModel.GetDeviceCodeAsync();
        }

        #endregion


        #region Публичные поля

        public DeviceCodeRequest DeviceCodeRequest {
            get;
            set; }

        public int CountdownCounter { get; set; }

        public RelayCommand RefreshCodeCommand
        {
            get
            {
                return refreshCodeCommand ??
                       (refreshCodeCommand = new RelayCommand(obj =>
                       {
                           authorizationModel.GetDeviceCodeAsync();
                       }));
            }
        }

        #endregion

        private async Task ShowTestDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "No wifi connection",
                Content = "Check your connection and try again.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        #region События

        void AuthorizationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DeviceCodeRequest":
                    DeviceCodeRequest = ((AuthorizationModel)sender).DeviceCodeRequest;
                    //if ((int)DeviceCodeRequest.StatusCode == 0)
                    //{
                    //    // @todo Реализовать ошибку загрузки кода
                    //};
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