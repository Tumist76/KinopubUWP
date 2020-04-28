using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Kinopub.UI.ViewModels;
using Windows.System;
using Kinopub.UI.Controls;
using Kinopub.Api.Entities.VideoContent;
using Windows.UI.Core;
using Kinopub.UI.Resources;
using Windows.UI.ViewManagement;
using System.Diagnostics;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kinopub.UI.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MediaPlayerPage : Page
    {
        public MediaPlayerPage()
        {
            //var currentView = SystemNavigationManager.GetForCurrentView();
            //currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Disabled;
            ////Назначение кнопок на функцию возвращения назад
            //KeyboardAccelerator GoBack = new KeyboardAccelerator
            //{
            //    Key = VirtualKey.GoBack
            //};
            //GoBack.Invoked += BackInvoked;
            //KeyboardAccelerator AltLeft = new KeyboardAccelerator
            //{
            //    Key = VirtualKey.Left
            //};
            //AltLeft.Invoked += BackInvoked;
            //this.KeyboardAccelerators.Add(GoBack);
            //this.KeyboardAccelerators.Add(AltLeft);
            //// ALT routes here
            //AltLeft.Modifiers = VirtualKeyModifiers.Menu;

            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (VideoItemVM)e.Parameter;
            ((MediaPlayerVM)DataContext).VideoItem = parameter;

            ConstantStrings strings = new ConstantStrings();
            var random = new Random();
            int index = random.Next(strings.LoadingPhrasesList.Count);
            RandomPhrase.Text = (strings.LoadingPhrasesList[index]);

            MainPlayer.MediaPlayer.PlaybackSession.Position = ((MediaPlayerVM) DataContext).LastPlayedPosition;
        }


        #region Навигация назад

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                //Избавляемся от инициализированных объектов
                ((MediaPlayerVM)DataContext).Dispose();
                //MainPlayer.MediaPlayer.Dispose(); //Вызывает вылет при попытке вернуться по стеку навигации
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        #endregion

        private void VideoPlaybackControls_OnGoToStartPositionButtonClicked(object sender, RoutedEventArgs e)
        {
            MainPlayer.MediaPlayer.PlaybackSession.Position = TimeSpan.MinValue;
        }

        public async void GoToCompactOverlayMode()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4))
            {
                Debug.WriteLine("Windows.ApplicationModel.Calls.CallsVoipContract v1.x found");
                if (ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay))
                {
                    // Supported
                    if (ApplicationView.GetForCurrentView().ViewMode == ApplicationViewMode.Default)
                    {
                        await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
                    }
                    else
                    {
                        await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
                    }
                }
                else
                {
                    Debug.WriteLine("ApplicationViewMode.CompactOverlay not supported");
                }
            }
        }

        private void VideoPlaybackControls_GoToCompactOverlayModeClicked(object sender, RoutedEventArgs e)
        {
            GoToCompactOverlayMode();
        }
    }
}
