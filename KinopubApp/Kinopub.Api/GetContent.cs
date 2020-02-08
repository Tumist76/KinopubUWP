using KinopubApi.Settings;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Kinopub.Api.Entities.VideoContent.VideoItem;
using Kinopub.Api.Entities.VideoContent.VideoItem.TypesConstants;
using RestSharp;
using Kinopub.Api.Tools;
using Newtonsoft.Json;
using RestSharp.Portable.Deserializers;

namespace Kinopub.Api
{
    public class GetContent
    {
        public GetContent(string accessToken)
        {
            AccessToken = string.Format("Bearer " + accessToken);
        }

        private string AccessToken;

        #region Методы получения видео

        /// <summary>
        /// Выполняет поиск видео по названию
        /// </summary>
        /// <param name="title"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<SearchEntity> SearchItems
            (string title, int itemsPerPage, int page)
        {
            var request = BuildRequest("", ContentTypeEnum.Empty, itemsPerPage, page);
            request.AddParameter("title", title);

            var restResponse = await GetRestClient().ExecuteTaskAsync<SearchEntity>(request);
            return restResponse.Data;
        }

        /// <summary>
        /// Получает свежие видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<SearchEntity> GetFreshItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("fresh", contentType, itemsPerPage, page);
            var restResponse = await GetRestClient().ExecuteTaskAsync<SearchEntity>(request);
            return restResponse.Data;
        }
        /// <summary>
        /// Получает горячие видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<SearchEntity> GetHotItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("hot", contentType, itemsPerPage, page);
            var restResponse = await GetRestClient().ExecuteTaskAsync<SearchEntity>(request);

            // @todo Сделать нормальный таймаут и обработчик ошибок
            //@body При некорректном запросе здесь приложение просто вешается. Так быть не должно. 
            //Нужно корректная обработка ошибок с выводом тех, которые могут быть важны пользователю, на экран.
            ErrorHandler.CheckResult(restResponse);
            return restResponse.Data;
        }

        private void CheckResponse(IRestResponse restResponse)
        {
            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                //@todo заняться тут исключениями
            }
        }

        /// <summary>
        /// Получает новые видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<SearchEntity> GetNewItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("new", contentType, itemsPerPage, page);
            var restResponse = await GetRestClient().ExecuteTaskAsync<SearchEntity>(request);
            return restResponse.Data;
        }
        /// <summary>
        /// Найти похожие видео
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<SearchEntity> GetSimilarItems(int itemId)
        {
            var request = new RestRequest("/v1/items/similar", Method.GET);
            request.AddParameter("id", itemId);

            var restResponse = await GetRestClient().ExecuteTaskAsync<SearchEntity>(request);
            return restResponse.Data;
        }
        /// <summary>
        /// Получить конкретное видео
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<ItemContent> GetItem
            (long itemId)
        {
            var request = new RestRequest("/v1/items/" + itemId,
                Method.GET);

            request.AddHeader("Authorization",
                AccessToken);

            ////@todo поменять на асинхронную работу с одновременной десереализацией
            //var restResponse = GetRestClient().Execute(request);
            //var response = JsonConvert.DeserializeObject<GetItem>(restResponse.Content);

            var restResponse = await GetRestClient().ExecuteTaskAsync<ItemContent>(request);
            ErrorHandler.CheckResult(restResponse);
            return restResponse.Data;
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
        private RestRequest BuildRequest(string action, ContentTypeEnum type, int itemsPerPage, int page)
        {
            //Если тип подраздела items не пустой, то добавляем подраздел запроса
            if (!String.IsNullOrEmpty(action))
                action = "/" + action;

            var request = new RestRequest("/v1/items" + action,
                Method.GET);

            request.AddHeader("Authorization",
                AccessToken);

            // @todo Сделать какой-то более красивый враппер для этого распределения
            string chosenType = ContentTypesConverter.GetContentType(type); //Пусть будет пока выбор по умолчанию такой
            if (!String.IsNullOrEmpty(chosenType)) request.AddParameter("type", chosenType);

            request.AddParameter("perpage", itemsPerPage);
            request.AddParameter("page", page);
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