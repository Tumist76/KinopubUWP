using Kinopub.Api;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.UI.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Kinopub.Api.Entities.VideoContent.VideoItem;

namespace Kinopub.UI.ViewModels
{
    public class SearchVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VideoItem> Results { get; set; }

        public string Query
        {
            get => query;
            set
            {
                query = value;
                GetResultsAsync();
            }
        }

        private string query;


        private async Task GetResultsAsync()
        {
            //Поиск на Кинопабе не работает меньше, чем с тремя символами
            if (query.Length < 3)
            {
                if (Results != null) Results.Clear();
                return;
            }
            var requestManager = new GetContent(AuthTokenManagementModel.GetAuthToken());
            var searchRequest = await requestManager.SearchItems(query, 25, 1);
            if (Results == null)
                Results = new ObservableCollection<VideoItem>();
            else
                Results.Clear();
            foreach (var item in searchRequest.Items)
            {
                Results.Add(item);
            }
        }
    }
}