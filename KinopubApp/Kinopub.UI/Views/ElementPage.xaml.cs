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
using Kinopub.Api.Entities.VideoContent;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kinopub.UI.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ElementPage : Page
    {
        public ElementPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (long)e.Parameter;
            ((VideoItemVM)DataContext).ItemId = parameter;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //@todo в модели добавить свойство ссылки на поток, получающейся динамически для фильмов и сериалов
            var video = ((VideoItemVM)DataContext).ItemProperties.Videos.FirstOrDefault();
            PlayVideo(video);
        }

        private void PlayVideo(Video videoToPlay)
        {
            //@todo Стоит переделать как-то иначе, чтобы вызов производился из ВМки и метод проверки был один

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MediaPlayerPage), videoToPlay);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Video)e.ClickedItem;
            PlayVideo(clickedItem);
        }
    }
}
