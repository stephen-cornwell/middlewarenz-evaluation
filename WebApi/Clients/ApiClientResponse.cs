using System.Net;

namespace MiddlewareNz.Evaluation.WebApi.Clients
{
    /// <summary>
    /// Generic response for api clients returning a payload.
    /// Wraps both successful and failure responses. 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class ApiClientResponse<TContent> 
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string ResponseReason { get; protected set; }
        public TContent Content { get; protected set; }

        public ApiClientResponse(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public ApiClientResponse(HttpStatusCode httpStatusCode, TContent content)
        {
            HttpStatusCode = httpStatusCode;
            Content = content;
        }

        public ApiClientResponse(HttpStatusCode httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            ResponseReason = message;
        }
    }
}