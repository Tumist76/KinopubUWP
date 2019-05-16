using System.Threading;
using System.Threading.Tasks;
using KinopubApi.Entities.Auth;
using KinopubApi.Settings;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace Kinopub.Api
{
    public static class Auth
    {
        public static async Task<IRestResponse<DeviceCodeRequest>> GetDeviceCodeAsync(string deviceId, string clientSecret)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new RestClient(Constants.Domain);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("/oauth2/device", Method.POST);
            request.AddParameter("grant_type", "device_code"); //Выбираем тип последующей авторизации - код устройства
            request.AddParameter("client_id", deviceId);
            request.AddParameter("client_secret", clientSecret);

            // async with deserialization
            var restResponse = await client.Execute<DeviceCodeRequest>(request, cancellationTokenSource.Token);
            return restResponse;
        }
    }
}
