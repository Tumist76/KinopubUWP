using Kinopub.Api.Entities.VideoContent;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.Storage.Search;
using Kinopub.Api;
using Kinopub.UI.Models;

namespace Kinopub.UI.ViewModels
{
    public class SearchSuggestBoxVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VideoItem> QuickResults { get; set; }

        public string Query
        {
            get => query;
            set
            {
                query = value;
                GetQuickResults();
            }
        }

        private string query;

        private async void GetQuickResults()
        {
            //Поиск на Кинопабе не работает меньше, чем с тремя символами
            if (query.Length < 3)
            {
                if (QuickResults != null) QuickResults.Clear();
                return;
            }
            var requestManager = new GetContent(AuthTokenManagementModel.GetAuthToken());
            var searchRequest = await requestManager.SearchItems(query, 5, 1);
            if (QuickResults == null)
                QuickResults = new ObservableCollection<VideoItem>();
            else
                QuickResults.Clear();
            foreach (var item in searchRequest.Items)
            {
                QuickResults.Add(item);
            }

        }
    }
}