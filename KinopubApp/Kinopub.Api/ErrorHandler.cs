using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace Kinopub.Api
{
    public static class ErrorHandler
    {
        // @todo Написать логику хендлера для разных ошибок. Устранимые ошибки должны автоматически перезапускать метод запроса
        public static void CheckResult(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //AuthTokenManagementModel.Re();
            }
        }
    }
}
