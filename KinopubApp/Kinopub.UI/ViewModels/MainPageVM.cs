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

namespace Kinopub.UI.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VideoItem> Items { get; set; }

        public MainPageVM()
        {
            var itemsList = new GetContent(AuthTokenManagementModel.GetAuthToken())
                .GetHotItems(ContentTypeEnum.Movie, 1, 1).Result.Items;
            Items = new ObservableCollection<VideoItem>(itemsList);
        }
    }
}
