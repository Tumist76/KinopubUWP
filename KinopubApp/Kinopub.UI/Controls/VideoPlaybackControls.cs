using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Kinopub.Api.Entities.VideoContent;

namespace Kinopub.UI.Controls
{
    public sealed class VideoPlaybackControls : MediaTransportControls, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<EventArgs> QualitySelectionButtonClick;

        public static readonly DependencyProperty AvaliableQualitiesProperty =
            DependencyProperty.Register(
                "AvaliableQualities", 
                typeof(List<uint>),
                typeof(VideoPlaybackControls),
                new PropertyMetadata(
                    false,
                    new PropertyChangedCallback(OnQualitiesChanged))
                );

        public List<uint> AvaliableQualities
        {
            get
            {
                return (List<uint>)GetValue(AvaliableQualitiesProperty);
            }
            set
            {
                SetValue(AvaliableQualitiesProperty, value);
            }
        }

        private static void OnQualitiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VideoPlaybackControls playbackControls = d as VideoPlaybackControls;
            playbackControls.GetAddToPlaylistFlyout(playbackControls.qualitySelectionMenuFlyout);
        }

        private List<uint> qualities;

        private MenuFlyout qualitySelectionMenuFlyout;

        public VideoPlaybackControls()
        {
            this.DefaultStyleKey = typeof(VideoPlaybackControls);
        }

        private void GetAddToPlaylistFlyout(MenuFlyout flyout)
        {
            foreach (var item in AvaliableQualities)
            {
                flyout.Items.Add(new MenuFlyoutItem()
                {
                    Text = item.ToString()
                });
            }
        }

        protected override void OnApplyTemplate()
        {
            // This is where you would get your custom button and create an event handler for its click method.
            Button qualitySelectionButton = GetTemplateChild("QualitySelectionButton") as Button;
            qualitySelectionButton.Click += QualitySelectionButton_Click;

            qualitySelectionMenuFlyout = GetTemplateChild("QualitySelectionMenuFlyout") as MenuFlyout;
            

            base.OnApplyTemplate();
        }

        private void QualitySelectionButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise an event on the custom control when 'like' is clicked
            QualitySelectionButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
