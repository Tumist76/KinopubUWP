using KinopubApi.Entities.Auth;
using KinopubApi.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KinopubApi
{
    public static class Auth
    {
        public static IRestResponse<DeviceCodeRequest> GetDeviceCodeAsync(string deviceId, string clientSecret)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new RestClient(Constants.Domain);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("/oauth2/device", Method.POST);
            request.AddParameter("grant_type", "device_code"); //Выбираем тип последующей авторизации - код устройства
            request.AddParameter("client_id", deviceId);
            request.AddParameter("client_secret", clientSecret);

            // async with deserialization
            var restResponse = client.ExecutePostTaskAsync<DeviceCodeRequest>(request, cancellationTokenSource.Token);
            return restResponse.Result;
        }
    }
}
