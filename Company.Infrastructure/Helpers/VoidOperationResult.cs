using System.Net;

namespace Company.Infrastructure.Helpers
{
    public abstract class VoidOperationResult
    {
        /// <summary>
        /// Is error flag
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Message to return
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// It will indefify which status the operation has
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Success contructor
        /// </summary>
        /// <param name="message"></param>
        public VoidOperationResult(string message, HttpStatusCode httpStatusCode)
        {
            IsError = false;
            Message = message;
            StatusCode = httpStatusCode;
        }

        /// <summary>
        /// Fail constructor
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="message"></param>
        public VoidOperationResult(bool isError, string message, HttpStatusCode httpStatusCode)
        {
            IsError = isError;
            Message = message;
            StatusCode = httpStatusCode;
        }
    }
}
