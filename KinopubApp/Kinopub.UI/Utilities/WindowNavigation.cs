using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kinopub.UI.Utilities
{
    public class WindowNavigation
    {
        public static void WindowNavigateTo(Type type, LaunchActivatedEventArgs e)
        {
            // Create a Frame to act navigation context and navigate to the first page
            var rootFrame = new Frame();

            rootFrame.NavigationFailed += OnNavigationFailed;

            rootFrame.Navigate(type);

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();

            rootFrame.NavigationFailed -= OnNavigationFailed;

        }
        /// <summary>
        /// Вызывается в случае сбоя навигации на определенную страницу
        /// </summary>
        /// <param name="sender">Фрейм, для которого произошел сбой навигации</param>
        /// <param name="e">Сведения о сбое навигации</param>
        static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}
