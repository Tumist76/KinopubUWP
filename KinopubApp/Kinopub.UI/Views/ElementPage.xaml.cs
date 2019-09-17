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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var video = ((VideoItemVM)DataContext).ItemProperties.Videos.FirstOrDefault();
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MediaPlayerPage), video);
        }
    }
}
