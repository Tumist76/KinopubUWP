using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;

namespace Kinopub.UI.ViewModels
{
    class MediaPlayerVM
    {
        public MediaSource VideoMediaSource { get; set; }
        public string StreamUrl
        {
            get => streamUrl;
            set
            {
                streamUrl = value;
                //InitializeAdaptiveMediaSource(new Uri(streamUrl));
            }
        }

        private string streamUrl;

        private AdaptiveMediaSource ams;

        public MediaPlayerVM()
        {

        }
        // @todo Сделать нормальную реализацию адаптивного стриминга с автоматическим и ручным переключением
        //Ниже идёт сниппет иницализации адаптивного медиаисточника 
        //async private void InitializeAdaptiveMediaSource(System.Uri uri)
        //{
        //    AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri);

        //    if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
        //    {
        //        ams = result.MediaSource;
        //        VideoMediaSource = MediaSource.CreateFromAdaptiveMediaSource(ams);


        //        ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();

        //        //    //Register for download requests
        //        //    ams.DownloadRequested += DownloadRequested;

        //        //    //Register for download failure and completion events
        //        //    ams.DownloadCompleted += DownloadCompleted;
        //        //    ams.DownloadFailed += DownloadFailed;

        //        //    //Register for bitrate change events
        //        //    ams.DownloadBitrateChanged += DownloadBitrateChanged;
        //        //    ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

        //        //    //Register for diagnostic event
        //        //    ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
        //        //}
        //        //else
        //        //{
        //        //    // Handle failure to create the adaptive media source
        //        //    MyLogMessageFunction($"Adaptive source creation failed: {uri} - {result.ExtendedError}");
        //        //}
        //    }
        //}
    }
}
