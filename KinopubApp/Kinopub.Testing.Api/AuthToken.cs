using Kinopub.UI.Models;

namespace Kinopub.Testing.Api
{
    public class AuthToken
    {
        public static string GetAuthToken()
        {
            var authData = AuthTokenManagementModel.GetAuthData();
                    if (AuthTokenManagementModel.IsAccessTokenValid(authData))
                    {
                        return AuthTokenManagementModel.GetAuthToken();
                    }
                    else
                    {
                        if (AuthTokenManagementModel.IsRefreshTokenValid(authData))
                        {
                            AuthTokenManagementModel.RefreshAccessToken();
                            return AuthTokenManagementModel.GetAuthToken();
                        }
                        else
                        {
                            return null;
                        }
                    }
        }
    }
}