using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kinopub.UI.Controls
{
    public sealed class VideoPlaybackControls : MediaTransportControls
    {
        public event EventHandler<EventArgs> QualitySelectionButtonClick;


        public List<uint> Qualities { get; set; }

        private MenuFlyout qualitySelectionMenuFlyout;

        public VideoPlaybackControls()
        {
            this.DefaultStyleKey = typeof(VideoPlaybackControls);
        }

        public MenuFlyout GetAddToPlaylistFlyout()
        {
            MenuFlyout flyout = new MenuFlyout();
            foreach (var item in Qualities)
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
            foreach (var item in GetAddToPlaylistFlyout().Items)
            {
                qualitySelectionMenuFlyout.Items.Insert(qualitySelectionMenuFlyout.Items.Count, item);
            }

            base.OnApplyTemplate();
        }

        private void QualitySelectionButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise an event on the custom control when 'like' is clicked
            QualitySelectionButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
