using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kinopub.UI.Utilities
{
    /// <summary>
    /// Класс-менеджер для настроек и прочих хранимых данных приложения
    /// </summary>
    public class SettingsManager
    {
        public static object GetLocalSetting(string settingName)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            return localSettings.Values[settingName];
        }

        public static object GetLocalCompositeSetting(string containerName, string settingName)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite =
                (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values[containerName];
            if (composite == null)
            {
                return null;
            }
            else
            {
                return composite[settingName];
            }
        }
        public static ApplicationDataCompositeValue GetLocalCompositeContainer(string containerName)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            ApplicationDataCompositeValue composite =
                (ApplicationDataCompositeValue)localSettings.Values[containerName];
            if (composite != null)
            {
                return composite;
            }
            return null;
        }

        public static void SetLocalSetting(string settingName, object value)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[settingName] = value;
        }

        public static void SetLocalCompositeContainer(string containerName, ApplicationDataCompositeValue composite)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[containerName] = composite;
        }

    }
}
