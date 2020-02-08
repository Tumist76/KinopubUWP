using System.Collections.Generic;

namespace Kinopub.Api.Errors
{
    public class KinopubError
    {
        public string CallerMethod { get; set; }
        public KinopubErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> RequestParameters { get; set; }
    }
}