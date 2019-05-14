using KinopubApiNetCore.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KinopubApiNetCore.Auth
{
    class Auth
    {
        public static IRestResponse<DeviceCodeRequest> GetDeviceCodeAsync(string deviceId, string clientSecret)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new RestClient(Constants.Domain);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("oauth2/device", Method.POST);
            request.AddParameter("grant_type", "device_code"); //Выбираем тип последующей авторизации - код устройства


            // async with deserialization
            var restResponse = client.ExecutePostTaskAsync<DeviceCodeRequest>(request, cancellationTokenSource.Token);
            return restResponse.Result;
        }

    }
}
