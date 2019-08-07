using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;

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
                VideoMediaSource = MediaSource.CreateFromUri(new Uri(streamUrl));
            }
        }

        private string streamUrl;

        public MediaPlayerVM()
        {

        }
    }
}
