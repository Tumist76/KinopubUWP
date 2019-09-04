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

namespace Kinopub.UI.Controls
{
    public sealed class VideoPlaybackControls : MediaTransportControls, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<EventArgs> QualitySelectionButtonClick;

        public static readonly DependencyProperty AvaliableQualititesProperty =
            DependencyProperty.Register(
                "AvaliableQualitites", 
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
                return (List<uint>)GetValue(AvaliableQualititesProperty);
            }
            set
            {
                SetValue(AvaliableQualititesProperty, value);
            }
        }

        //public List<uint> Qualities
        //{
        //    get => qualities;
        //    set
        //    {
        //        qualities = value;
        //        foreach (var item in GetAddToPlaylistFlyout().Items)
        //        {
        //            qualitySelectionMenuFlyout.Items.Insert(qualitySelectionMenuFlyout.Items.Count, item);
        //        }
        //    }
        //}


        private static void OnQualitiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("Boop");
        }

        private List<uint> qualities;

        private MenuFlyout qualitySelectionMenuFlyout;

        public VideoPlaybackControls()
        {
            this.DefaultStyleKey = typeof(VideoPlaybackControls);
        }

        public MenuFlyout GetAddToPlaylistFlyout()
        {
            MenuFlyout flyout = new MenuFlyout();
            foreach (var item in qualities)
            {
                flyout.Items.Add(new MenuFlyoutItem()
                {
                    Text = item.ToString()
                });
            }
            //flyout.Items.Add(new MenuFlyoutItem()
            //{
            //    Icon = new FontIcon() { Glyph = "\uEC4F" },
            //    Text = "Now Playing"
            //});
            //flyout.Items.Add(new MenuFlyoutSeparator());
            //flyout.Items.Add(new MenuFlyoutItem()
            //{
            //    Icon = new SymbolIcon(Symbol.Add),
            //    Text = "New Playlist"
            //});
            return flyout;
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
