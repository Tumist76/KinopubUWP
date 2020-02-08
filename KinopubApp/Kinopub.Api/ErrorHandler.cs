using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Kinopub.Api.Errors;
using Newtonsoft.Json;
using RestSharp;

namespace Kinopub.Api
{
    public static class ErrorHandler
    {
        // @todo Написать логику хендлера для разных ошибок. Устранимые ошибки должны автоматически перезапускать метод запроса
        public static void CheckResult(IRestResponse response)
        {
            if (response.ErrorException == null 
                && response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            StackTrace stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod().Name;
            if (response.ErrorException != null)
            {

                var ex = new KinopubApiException(KinopubErrorType.NoConnection);
                ex.Error.CallerMethod = callerMethod;
                throw ex;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var ex = new KinopubApiException(KinopubErrorType.Unauthorized);
                ex.Error.CallerMethod = callerMethod;
                throw ex;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var ex = new KinopubApiException(KinopubErrorType.Unauthorized);
                ex.Error.CallerMethod = callerMethod;
                throw ex;
            }
        }

    }
}
