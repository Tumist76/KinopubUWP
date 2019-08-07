using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Kinopub.UI.ViewModels;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.UI.Views;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Kinopub.UI
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (VideoItem)e.ClickedItem;
            var itemId = clickedItem.Id;
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(ElementPage), itemId);
            // @todo Перенести переход на другую страницу с code-behind в MVVM. Или нет, потому что добавляет сложности?
            //var viewModel = (MainPageVM)DataContext;
            //if (viewModel.MyCommand.CanExecute(null))
            //    viewModel.MyCommand.Execute(null);
        }
    }
}
