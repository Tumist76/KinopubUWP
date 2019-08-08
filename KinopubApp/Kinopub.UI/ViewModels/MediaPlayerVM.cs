using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                VideoMediaSource = MediaSource.CreateFromUri(new Uri(streamUrl));
            }
        }

        private string streamUrl;

        private AdaptiveMediaSource ams;

        public MediaPlayerVM()
        {

        }
        // @todo Сделать нормальную реализацию адаптивного стриминга с автоматическим и ручным переключением
        //Ниже идёт сниппет иницализации адаптивного медиаисточника 
        private void InitializeAdaptiveMediaSource(System.Uri uri)
        {
            AdaptiveMediaSourceCreationResult result = AdaptiveMediaSource.CreateFromUriAsync(uri).GetResults();

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                ams = result.MediaSource;
                VideoMediaSource = MediaSource.CreateFromAdaptiveMediaSource(ams);


                ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();
                foreach (var bitrate in ams.AvailableBitrates)
                {
                    Debug.WriteLine(bitrate.ToString());
                }

            }
        }
    }
}
