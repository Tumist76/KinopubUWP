using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.UI.Entities.M3u8;

namespace Kinopub.UI.Controls
{
    public sealed class VideoPlaybackControls : MediaTransportControls, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<EventArgs> QualitySelectionButtonClicked;
        public event RoutedEventHandler GoBackButtonClicked;

        #region DependencyProperties

        public static readonly DependencyProperty AvaliableQualitiesProperty =
            DependencyProperty.Register(
                "AvaliableStreams", 
                typeof(List<M3u8Stream>),
                typeof(VideoPlaybackControls),
                new PropertyMetadata(
                    false,
                    new PropertyChangedCallback(OnQualitiesChanged))
                );

        public static readonly DependencyProperty SelectedBandwidthProperty =
            DependencyProperty.Register(
                "SelectedBandwidth",
                typeof(uint),
                typeof(VideoPlaybackControls),
                new PropertyMetadata(
                    false,
                    null)
            );

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(VideoPlaybackControls), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginalTitleProperty =
            DependencyProperty.Register("OriginalTitle", typeof(string), typeof(VideoPlaybackControls), new PropertyMetadata(null));

        #endregion

        #region Public Properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string OriginalTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public List<M3u8Stream> AvaliableStreams
        {
            get { return (List<M3u8Stream>)GetValue(AvaliableQualitiesProperty); }
            set { SetValue(AvaliableQualitiesProperty, value); }
        }

        //Если "0", то битрейт выбирается автоматически плеером
        public uint SelectedBandwidth
        {
            get { return (uint)GetValue(SelectedBandwidthProperty);  }
            set { SetValue(SelectedBandwidthProperty, value); }
        }

        #endregion

        private static void OnQualitiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VideoPlaybackControls playbackControls = d as VideoPlaybackControls;
            playbackControls.GetAddToPlaylistFlyout(playbackControls.qualitySelectionMenuFlyout);
        }

        private static void OnSelectedBandwithChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VideoPlaybackControls playbackControls = d as VideoPlaybackControls;
            //@todo проверить работу этой части кода, потому что, ЕМНИП, жирным становится всё равно только "Авто"
            playbackControls.qualitySelectionMenuFlyout.Items.Where(x => (uint)x.DataContext == playbackControls.SelectedBandwidth).FirstOrDefault().FontWeight = FontWeights.Bold;
        }

        private List<uint> qualities;

        private MenuFlyout qualitySelectionMenuFlyout;

        public VideoPlaybackControls()
        {
            this.DefaultStyleKey = typeof(VideoPlaybackControls);
        }

        private void GetAddToPlaylistFlyout(MenuFlyout flyout)
        {

            var autoMenuItem = new MenuFlyoutItem()
            {
                DataContext = uint.MinValue, //При нуле обработчик ставит динамическое изменение битрейта
                Text = "Авто",
                FontWeight = FontWeights.Bold
            };
            autoMenuItem.Click += QualityFlyoutMenuItem_Click;
            qualitySelectionMenuFlyout.Items.Add(autoMenuItem);

            foreach (var item in AvaliableStreams)
            {
                var menuItem = new MenuFlyoutItem()
                {
                    DataContext = item.Bandwidth,
                    Text = item.Resolution
                };
                menuItem.Click += QualityFlyoutMenuItem_Click;
                qualitySelectionMenuFlyout.Items.Add(menuItem);
            }
        }

        protected override void OnApplyTemplate()
        {
            // This is where you would get your custom button and create an event handler for its click method.
            Button qualitySelectionButton = GetTemplateChild("QualitySelectionButton") as Button;
            qualitySelectionButton.Click += QualitySelectionButton_Click;
            qualitySelectionMenuFlyout = GetTemplateChild("QualitySelectionMenuFlyout") as MenuFlyout;
            
            Button goBackButton = GetTemplateChild("GoBackButton") as Button;
            goBackButton.Click += GoBackButton_Click;

            base.OnApplyTemplate();
        }

        private void QualitySelectionButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise an event on the custom control when 'like' is clicked
            QualitySelectionButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBackButtonClicked?.Invoke(this, new RoutedEventArgs());
        }

        private void QualityFlyoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = sender as MenuFlyoutItem;
            SelectedBandwidth = ((uint) clickedMenuItem.DataContext);
            //Убираем выделение всех пунктов
            foreach (var item in qualitySelectionMenuFlyout.Items)
            {
                item.FontWeight = FontWeights.Normal;
            }
            //и добавляем жирности "активному"
            clickedMenuItem.FontWeight = FontWeights.Bold;
        }
    }
}
