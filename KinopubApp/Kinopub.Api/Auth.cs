using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Graphics.Printing;
using Kinopub.Api.Entities.Auth;
using KinopubApi.Settings;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using Newtonsoft.Json;

namespace Kinopub.Api
{
    public static class Auth
    {
        public static async Task<IRestResponse<DeviceCodeRequest>> GetDeviceCodeAsync(string deviceId, string clientSecret)
        {
            //Тип запроса - получение кода устройства
            var request = BuildAuthRequest("device_code", deviceId, clientSecret);

            // асихронно с десериализацией
            var restResponse = await GetRestClient().Execute<DeviceCodeRequest>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }

        public static async Task<AccessTokenRequest> GetAccessTokenAsync
            (string deviceId, string clientSecret, string code)
        {
            //Тип запроса - получение токена
            var request = BuildAuthRequest("device_token", deviceId, clientSecret);
            request.AddParameter("Code", code);

            var restResponse = await GetRestClient().Execute(request, GetCancelletionTokenSource().Token);
            if (restResponse.IsSuccess)
            {
                
            }
            else
            {
                return null;
            }

            var tokenRequestData = JsonConvert.DeserializeObject<AccessTokenRequest>(restResponse.Content);
            return tokenRequestData;
        }

        public static async Task<IRestResponse<AccessTokenRequest>> RefreshTokenAsync
            (string deviceId, string clientSecret, string refreshToken)
        {
            //Тип запроса - обновление токена доступа
            var request = BuildAuthRequest("refresh_token", deviceId, clientSecret);
            request.AddParameter("refresh_token", refreshToken);

            return await GetRestClient().Execute<AccessTokenRequest>(request, GetCancelletionTokenSource().Token);
        }

        /// <summary>
        /// Строит базовый запрос на с выбранным типом
        /// </summary>
        /// <param name="grantType">Тип запроса</param>
        /// <param name="deviceId">Идентификатор устройства</param>
        /// <param name="clientSecret">Секретный код клиента</param>
        /// <returns></returns>
        private static RestRequest BuildAuthRequest(string grantType, string deviceId, string clientSecret)
        {
            var request = new RestRequest("/oauth2/device", Method.POST);
            request.AddParameter("grant_type", grantType); //Выбираем тип последующей авторизации
            request.AddParameter("client_id", deviceId);
            request.AddParameter("client_secret", clientSecret);

            return request;
        }

        private static CancellationTokenSource GetCancelletionTokenSource()
        {
            return new CancellationTokenSource();
        }

        private static RestClient GetRestClient()
        {
            var client = new RestClient(Constants.Domain);
            //Ставим таймаут на запрос - 15 секунд
            client.Timeout = new TimeSpan(0, 0, 0, 15);
            return client;
        }
    }
}
