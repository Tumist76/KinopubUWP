﻿using System;
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

        public ObservableCollection<VideoItem> PopularMovies { get; set; }

        public ObservableCollection<VideoItem> PopularTvShows { get; set; }

        public HomePageVM()
        {
            GetItems();
        }

        private async void GetItems()
        {
            var requestManager = new GetContent(AuthTokenManagementModel.GetAuthToken());

            var hotMoviesList = await requestManager.GetHotItems(ContentTypeEnum.Movie, 50, 1);
            var hotTvShowsList = await requestManager.GetHotItems(ContentTypeEnum.Serial, 10, 1);
            PopularMovies = new ObservableCollection<VideoItem>(hotMoviesList.Items);
            PopularTvShows = new ObservableCollection<VideoItem>(hotTvShowsList.Items);
        }
    }
}
