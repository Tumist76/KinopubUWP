using System.Threading.Tasks;
using Kinopub.Api;
using Kinopub.Api.Entities.UserItem;
using Kinopub.UI.Models;

namespace Kinopub.UI.ViewModels
{
    public class UserInfoVM
    {
        public UserInfoVM()
        {
            GetUserAsync();
        }

        public User User { get; set; }

        private async Task GetUserAsync()
        {
            var requestManager = new GetUser(AuthTokenManagementModel.GetAuthToken());

            User = await requestManager.GetUserInfo();
        }
    }
}