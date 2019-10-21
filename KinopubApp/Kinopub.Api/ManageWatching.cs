using KinopubApi.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;
using RestSharp;
using Kinopub.Api.Tools;
using Newtonsoft.Json;
using RestSharp.Portable.Deserializers;

namespace Kinopub.Api
{
    public class ManageWatching
    {
        public ManageWatching(string accessToken)
        {
            AccessToken = string.Format("Bearer " + accessToken);
        }

        private string AccessToken;

        #region Методы получения недосмотренных фильмов/сериалов

        /// <summary>
        /// Получить недосмотренные фильмы
        /// </summary>
        /// <returns></returns>
        public async Task<RootWatchingMovie> GetWatchingMovies()
        {
            var request = BuildRequest("movies");

            var restResponse = await GetRestClient().ExecuteTaskAsync<RootWatchingMovie>(request);
            return restResponse.Data;
        }

        /// <summary>
        /// Получить недосмотренные сериалы из "буду смотреть"
        /// </summary>
        /// <returns></returns>
        public async Task<RootWatchingSerial> GetWatchingSubscribedSerials()
        {
            var request = BuildRequest("serials");
            request.AddParameter("subscribed", 1);

            var restResponse = await GetRestClient().ExecuteTaskAsync<RootWatchingSerial>(request);
            return restResponse.Data;
        }

        /// <summary>
        /// Получить недосмотренные сериалы
        /// </summary>
        /// <returns></returns>
        public async Task<RootWatchingSerial> GetWatchingSerials()
        {
            var request = BuildRequest("serials");

            var restResponse = await GetRestClient().ExecuteTaskAsync<RootWatchingSerial>(request);
            return restResponse.Data;
        }

        public async Task<WatchingItem> GetWatchingItem(int id)
        {
            var request = BuildRequest(null);
            request.AddParameter("id", id);

            var restResponse = await GetRestClient().ExecuteTaskAsync<RootWatchingItem>(request);
            return restResponse.Data.WatchingItem;
        }

        private void CheckResponse(IRestResponse restResponse)
        {
            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                //@todo заняться тут исключениями
            }
        }

        #endregion

        #region Методы построения базы запроса

        /// <summary>
        /// Строит базовый запрос с токеном авторизации и параметрами страниц
        /// </summary>
        /// <param name="action"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private RestRequest BuildRequest(string action)
        {
            //Если тип подраздела watching не пустой, то добавляем подраздел запроса
            if (!String.IsNullOrEmpty(action))
                action = "/" + action;

            var request = new RestRequest("/v1/watching" + action,
                Method.GET);

            request.AddHeader("Authorization",
                AccessToken);
            return request;
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

        #endregion
    }
}