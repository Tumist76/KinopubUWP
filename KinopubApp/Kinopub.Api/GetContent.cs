using KinopubApi.Settings;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Kinopub.Api.Entities.VideoContent;
using Kinopub.Api.Entities.VideoContent.TypesConstants;

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
        public async Task<IRestResponse<SearchEntity>> SearchItems
            (string title, int itemsPerPage, int page)
        {
            var request = BuildRequest("search", itemsPerPage, page);
            request.AddParameter("title", title);

            var restResponse = await GetRestClient().Execute<SearchEntity>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }

        /// <summary>
        /// Получает свежие видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IRestResponse<SearchEntity>> GetFreshItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("fresh", itemsPerPage, page);
            var restResponse = await GetRestClient().Execute<SearchEntity>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }
        /// <summary>
        /// Получает горячие видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IRestResponse<SearchEntity>> GetHotItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("hot", itemsPerPage, page);
            var restResponse = await GetRestClient().Execute<SearchEntity>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }
        /// <summary>
        /// Получает новые видео выбранного типа
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IRestResponse<SearchEntity>> GetNewItems
            (ContentTypeEnum contentType, int itemsPerPage, int page)
        {
            var request = BuildRequest("new", itemsPerPage, page);
            var restResponse = await GetRestClient().Execute<SearchEntity>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }

        public async Task<IRestResponse<SearchEntity>> GetSimilarItems(int itemId)
        {
            var request = new RestRequest("/v1/items/similar", Method.GET);
            request.AddParameter("id", itemId);

            var restResponse = await GetRestClient().Execute<SearchEntity>(request, GetCancelletionTokenSource().Token);
            return restResponse;
        }
        /// <summary>
        /// Получить конкретное видео
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<IRestResponse<VideoItem>> GetItem
            (int itemId)
        {
            var request = new RestRequest("/v1/items/" + itemId,
                Method.GET);

            request.AddParameter("Authorization",
                AccessToken,
                ParameterType.HttpHeader);

            var restResponse = await GetRestClient().Execute<VideoItem>(request, GetCancelletionTokenSource().Token);
            return restResponse;
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
        private RestRequest BuildRequest(string action, int itemsPerPage, int page)
        {
            var request = new RestRequest("/v1/items/" + action,
                Method.GET);

            request.AddParameter("Authorization",
                AccessToken,
                ParameterType.HttpHeader);
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
            var client = new RestClient(Constants.Domain);
            //Ставим таймаут на запрос - 15 секунд
            client.Timeout = new TimeSpan(0, 0, 0, 15);
            return client;
        }

        #endregion
    }
}