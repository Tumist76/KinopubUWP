namespace Kinopub.Api.Errors
{
    public class KinopubApiException : System.Exception
    {
        public KinopubApiException(KinopubErrorType errorType)
        {
            Error = new KinopubError();
            Error.ErrorType = errorType;
        }
        public KinopubError Error { get; set; }
    }
}