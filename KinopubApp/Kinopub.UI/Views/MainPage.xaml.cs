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
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.VideoItem;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kinopub.UI.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MoveToMainPage();
        }

        private void MoveToMainPage()
        {
            MainNavView.SelectedItem = MainNavView.MenuItems.ElementAt(0);
            contentFrame.Navigate(typeof(HomePage));
        }

        private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item != null)
            {
                switch (item.Tag)
                {
                    case "Nav_HomePage":
                        contentFrame.Navigate(typeof(HomePage));
                        break;
                    case "Nav_WatchingPage":
                        contentFrame.Navigate(typeof(WatchingPage));
                        break;
                }
            }
        }

        private void MainNavView_BackRequested(NavigationView sender,
            NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
            KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!contentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (MainNavView.IsPaneOpen &&
                (MainNavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 MainNavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            contentFrame.GoBack();
            return true;
        }

        private bool isQueryFromSuggestion;
        private void NavViewSearchBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            isQueryFromSuggestion = true;
            var selectedMedia = (VideoItem) args.SelectedItem;
            contentFrame.Navigate(typeof(ElementPage), selectedMedia.Id);
        }

        private void NavViewSearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (isQueryFromSuggestion)
            {
                isQueryFromSuggestion = false;
                return;
            }
            contentFrame.Navigate(typeof(SearchPage), NavViewSearchBox.Text);
        }

        private void ProfileMenuItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ProfileFlyout.IsOpen)
                ProfileFlyout.Hide();
            else
                ProfileFlyout.ShowAt((NavigationViewItem)sender);
        }
    }
}
