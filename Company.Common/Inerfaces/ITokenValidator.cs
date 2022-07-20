using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Company.Common.Inerfaces
{
    public interface ITokenValidator
    {
        /// <summary>
        /// Return the user id from the claims from token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<int> ReturnUserId(HttpRequest req);
    }
}
