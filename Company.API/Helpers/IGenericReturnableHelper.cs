using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Company.API.Helpers
{
    public interface IGenericReturnableHelper
    {
        IActionResult GenericReturnableObject<T>(HttpStatusCode httpStatusCode, T data);
    }
}
