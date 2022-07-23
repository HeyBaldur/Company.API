using Company.Common.Inerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoundersController : Controller
    {
        private readonly ITokenValidator _tokenValidator;

        public FoundersController(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }
    }
}
