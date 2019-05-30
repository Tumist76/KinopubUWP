using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinopub.Api.Entities;
using Newtonsoft.Json;
using RestSharp.Portable;

namespace Kinopub.Api
{
    public static class ErrorHandler
    {
        public static Error GetErrorAsResult(IRestResponse response)
        {
            var error = JsonConvert.DeserializeObject<Error>(response.Content);
            return error;
        }
    }
}
