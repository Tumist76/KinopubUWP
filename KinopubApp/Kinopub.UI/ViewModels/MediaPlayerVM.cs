﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;
using Kinopub.UI.Entities.M3u8;
using Kinopub.UI.Models;

namespace Kinopub.UI.ViewModels
{
    class MediaPlayerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MediaSource VideoMediaSource { get; set; }

        public string StreamUrl
        {
            get => streamUrl;
            set
            {
                streamUrl = value;
                InitializeAdaptiveMediaSource(new Uri(streamUrl));
            }
        }

        private string streamUrl;

        private AdaptiveMediaSource ams;

        public List<M3u8Stream> M3u8Streams { get; set; }

        public uint SelectedBandwidth
        {
            get => selectedBandwidth;
            set
            {
                selectedBandwidth = value;
                SelectedBandwidth_PropertyChanged();
            }
        }

        private uint selectedBandwidth;

        public MediaPlayerVM()
        {
        }

        // @todo Сделать нормальную реализацию адаптивного стриминга с автоматическим и ручным переключением
        //Ниже идёт сниппет иницализации адаптивного медиаисточника
        async private void InitializeAdaptiveMediaSource(System.Uri uri)
        {
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri);

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                ams = result.MediaSource;
                VideoMediaSource = MediaSource.CreateFromAdaptiveMediaSource(ams);

                ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();
                M3u8Streams = await M3u8StreamModel.GetStreams(uri);

                SelectedBandwidth = ams.AvailableBitrates.Max<uint>();


                //    //Register for download requests
                //    ams.DownloadRequested += DownloadRequested;

                //    //Register for download failure and completion events
                //    ams.DownloadCompleted += DownloadCompleted;
                //    ams.DownloadFailed += DownloadFailed;

                //    //Register for bitrate change events
                //    ams.DownloadBitrateChanged += DownloadBitrateChanged;
                    ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

                //    //Register for diagnostic event
                //    ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
                //}
                //else
                //{
                //    // Handle failure to create the adaptive media source
                //    MyLogMessageFunction($"Adaptive source creation failed: {uri} - {result.ExtendedError}");
                //}
            }
        }

        private void PlaybackBitrateChanged(AdaptiveMediaSource sender, AdaptiveMediaSourcePlaybackBitrateChangedEventArgs args)
        {
            Debug.WriteLine("Changed bitrate to " + sender.CurrentPlaybackBitrate);
        }

        void SelectedBandwidth_PropertyChanged()
        {
            if (SelectedBandwidth != uint.MinValue)
            {
                ams.InitialBitrate = SelectedBandwidth;
                ams.DesiredMaxBitrate = SelectedBandwidth;
                ams.DesiredMinBitrate = SelectedBandwidth;
            }
            else
            {
                ams.DesiredMaxBitrate = ams.AvailableBitrates.Max<uint>();
                ams.DesiredMinBitrate = ams.AvailableBitrates.Min<uint>();
                ams.InitialBitrate = ams.AvailableBitrates.Max<uint>();
            }
        }

        public void Dispose()
        {
            VideoMediaSource.Dispose();
            ams.Dispose();
        }
    }
}
