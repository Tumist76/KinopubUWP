using Kinopub.Api.Tools;
using KinopubApi.Settings;
using RestSharp;
using System.Threading;
using Kinopub.Api.Entities.UserItem;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kinopub.Api
{
    public class GetUser
    {
        public GetUser(string accessToken)
        {
            AccessToken = string.Format("Bearer " + accessToken);
        }

        private string AccessToken;

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<User> GetUserInfo ()
        {
            var request = new RestRequest("/v1/user",
                Method.GET);

            request.AddHeader("Authorization",
                AccessToken);

            // @todo @todo поменять на асинхронную работу с одновременной десереализацией
            var restResponse = GetRestClient().Execute(request);
            var response = JsonConvert.DeserializeObject<UserInfo>(restResponse.Content);
            return response.User;
        }

        private static CancellationTokenSource GetCancelletionTokenSource()
        {
            return new CancellationTokenSource();
        }

        private static RestClient GetRestClient()
        {
            var client = new RestClient(Constants.ApiDomain);
            //Ставим таймаут на запрос - 15 секунд
            client.Timeout = 15000;

            // Заменяем десериализатор Newtonsoft JSON Handler для использования
            client.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
            client.AddHandler("text/json", () => NewtonsoftJsonSerializer.Default);
            client.AddHandler("text/x-json", () => NewtonsoftJsonSerializer.Default);
            client.AddHandler("text/javascript", () => NewtonsoftJsonSerializer.Default);
            client.AddHandler("*+json", () => NewtonsoftJsonSerializer.Default);

            return client;
        }
    }
}