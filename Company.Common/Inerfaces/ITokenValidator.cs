using Microsoft.AspNetCore.Http;

namespace Company.Common.Inerfaces
{
    public interface ITokenValidator
    {
        /// <summary>
        /// Return the user id from the claims from token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string ReturnUserId(HttpRequest req);
    }
}
