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
using Windows.Media.Streaming.Adaptive;
using Windows.Media.Core;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Media.Playback;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kinopub.UI.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MediaPlayerPage : Page
    {
        private AdaptiveMediaSource ams;
        public MediaPlayerPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (string)e.Parameter;
            //((MediaPlayerVM)DataContext).StreamUrl = parameter;
            InitializeAdaptiveMediaSource(new Uri(parameter));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }
        async private void InitializeAdaptiveMediaSource(System.Uri uri)
        {
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri);

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                ams = result.MediaSource;
                PlayerElement.SetMediaPlayer(new MediaPlayer());
                PlayerElement.MediaPlayer.Source = MediaSource.CreateFromAdaptiveMediaSource(ams);
                PlayerElement.MediaPlayer.Play();


                //ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();

                ////Register for download requests
                //ams.DownloadRequested += DownloadRequested;

                ////Register for download failure and completion events
                //ams.DownloadCompleted += DownloadCompleted;
                //ams.DownloadFailed += DownloadFailed;

                ////Register for bitrate change events
                //ams.DownloadBitrateChanged += DownloadBitrateChanged;
                //ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

                ////Register for diagnostic event
                //ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
            }
            else
            {
                // Handle failure to create the adaptive media source
                Debug.WriteLine($"Adaptive source creation failed: {uri} - {result.ExtendedError}");
            }
        }
    }
}
