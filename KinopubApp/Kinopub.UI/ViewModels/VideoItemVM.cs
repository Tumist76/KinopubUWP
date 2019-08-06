using Kinopub.Api.Entities.VideoContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;
using Kinopub.Api;
using Kinopub.UI.Models;

namespace Kinopub.UI.ViewModels
{
    class VideoItemVM
    {
        public VideoItem ItemProperties { get; set; }

        public long ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                ItemProperties = new GetContent(AuthTokenManagementModel.GetAuthToken()).GetItem(itemId).Result;
            }
        }

        private long itemId;

        public VideoItemVM()
        {
            
        }
    }
}
