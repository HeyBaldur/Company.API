using System.Net;

namespace Company.Infrastructure.Helpers
{
    public class GenericOperationResponse<T> : OperationResponse<T>
    {
        /// <summary>
        /// Success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public GenericOperationResponse(T result, string message, HttpStatusCode httpStatusCode) : base(result, message, httpStatusCode) { }

        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="message"></param>
        public GenericOperationResponse(bool isError, string message, HttpStatusCode httpStatusCode) : base(isError, message, httpStatusCode) { }
    }
}
