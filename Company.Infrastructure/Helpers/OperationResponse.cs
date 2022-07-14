using System.Net;

namespace Company.Infrastructure.Helpers
{
    public abstract class OperationResponse<T> : VoidOperationResult
    {
        /// <summary>
        /// Result to return
        /// </summary>
        public virtual T Result { get; private set; }

        /// <summary>
        /// Success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public OperationResponse(T result, string message, HttpStatusCode httpStatusCode) : base(message, httpStatusCode)
        {
            Result = result;
        }

        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="message"></param>
        protected OperationResponse(bool isError, string message, HttpStatusCode httpStatusCode) : base(isError, message, httpStatusCode)
        {
            Message = message;
            IsError = isError;
        }
    }
}
