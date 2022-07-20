using Company.Common.Inerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
