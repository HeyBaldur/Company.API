using Company.API.Helpers;
using Company.Common.Inerfaces;
using Company.Infrastructure.Services;
using Company.Models.Dtos;
using Company.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusinessController : ControllerBase
    {
        private readonly CompanyService _company;
        protected readonly IGenericReturnableHelper _genericReturnableHelper;
        private readonly ITokenValidator _tokenValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="company"></param>
        /// <param name="genericReturnableHelper"></param>
        /// <param name="tokenValidator"></param>
        public BusinessController(
            CompanyService company,
            IGenericReturnableHelper genericReturnableHelper,
            ITokenValidator tokenValidator)
        {
            _company = company;
            _genericReturnableHelper = genericReturnableHelper;
            _tokenValidator = tokenValidator;
        }

        /// <summary>
        /// Create a new business. This route allows you to create multiple companies at the same time.
        /// Also you can add the founders to the same object.
        /// </summary>
        /// <param name="businesses">Object(s) for the company you want to save</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBusiness(List<BusinessDto> businesses)
        {
            var result = await _company.CreateCompanyAsync(businesses, _tokenValidator.ReturnUserId(Request));

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }

        /// <summary>
        /// Make a paginated query of the results of the companies.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("Query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Query(QueryCompanyParams query)
        {
            var userId = _tokenValidator.ReturnUserId(Request);

            var result = await _company.GetBusinesses(query, userId);

            return _genericReturnableHelper.GenericReturnableObject(System.Net.HttpStatusCode.OK, result);
        }
    }
}
