using System.Threading;
using System.Threading.Tasks;
using Kinopub.Api.Entities.Auth;
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

            //Тип запроса - получение кода устройства
            var request = BuildAuthRequest("device_code", deviceId, clientSecret);

            // асихронно с десериализацией
            var restResponse = await client.Execute<DeviceCodeRequest>(request, cancellationTokenSource.Token);
            return restResponse;
        }

        public static async Task<IRestResponse<AccessTokenRequest>> GetAccessTokenAsync
            (string deviceId, string clientSecret, string code)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new RestClient(Constants.Domain);

            //Тип запроса - получение токена
            var request = BuildAuthRequest("device_token", deviceId, clientSecret);
            request.AddParameter("code", code);

            var restResponse = await client.Execute<AccessTokenRequest>(request, cancellationTokenSource.Token);
            return restResponse;
        }

        public static async Task<IRestResponse<AccessTokenRequest>> RefreshTokenAsync
            (string deviceId, string clientSecret, string refreshToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new RestClient(Constants.Domain);

            //Тип запроса - обновление токена доступа
            var request = BuildAuthRequest("refresh_token", deviceId, clientSecret);
            request.AddParameter("refresh_token", refreshToken);

            var restResponse = await client.Execute<AccessTokenRequest>(request, cancellationTokenSource.Token);
            return restResponse;
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
            request.AddParameter("grant_type", grantType); //Выбираем тип последующей авторизации - получение токена
            request.AddParameter("client_id", deviceId);
            request.AddParameter("client_secret", clientSecret);

            return request;
        }
    }
}
