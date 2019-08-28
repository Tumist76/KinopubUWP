using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls.Primitives;
using Kinopub.Api;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using Kinopub.UI.Models;
using Kinopub.UI.Utilities;

namespace Kinopub.UI.ViewModels
{
    class HomePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VideoItem> Items { get; set; }

        // @todo Разобраться, как делать переход между страниц

        //public RelayCommand OpenItemPageCommand
        //{
        //    get
        //    {
        //        if (openItemPageCommand == null)
        //            openItemPageCommand = new RelayCommand(p => OpenItemPage(p));
        //        return openItemPageCommand;
        //    }
        //}

        //private Action<object> OpenItemPage(object itemId)
        //{
        //    return null;
        //}

        //private RelayCommand openItemPageCommand;

        public HomePageVM()
        {
            GetItems();
        }

        private async void GetItems()
        {
            var contentManager = new GetContent(AuthTokenManagementModel.GetAuthToken());

            var hotMoviesList = await contentManager.GetHotItems(ContentTypeEnum.Movie, 10, 1);
            Items = new ObservableCollection<VideoItem>(hotMoviesList.Items);
        }
    }
}
