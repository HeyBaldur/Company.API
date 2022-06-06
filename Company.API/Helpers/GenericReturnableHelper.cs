using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Company.API.Helpers
{
    public class GenericReturnableHelper : Controller, IGenericReturnableHelper
    {
        /// <summary>
        /// This method should be included in all the responses of the 
        /// controllers because it gives more context the UI in order
        /// to map HTTP responses
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpStatusCode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult GenericReturnableObject<T>(HttpStatusCode httpStatusCode, T data)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(data);
                case HttpStatusCode.BadRequest:
                    return BadRequest(data);
                case HttpStatusCode.Forbidden:
                    return Forbid();
                case HttpStatusCode.InternalServerError:
                    return StatusCode(((int)httpStatusCode), data);
                case HttpStatusCode.NotFound:
                    return NotFound(data);
                default:
                    break;
            }

            return Conflict();
        }
    }
}
