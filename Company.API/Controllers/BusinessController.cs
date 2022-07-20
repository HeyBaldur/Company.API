using Company.API.Helpers;
using Company.Common.Connection.v1;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Infrastructure.Services;
using Company.Models.Helpers;
using Company.Models.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly CompanyService _company;
        protected readonly IGenericReturnableHelper _genericReturnableHelper;

        public BusinessController(
            CompanyService company, 
            IGenericReturnableHelper genericReturnableHelper)
        {
            _company = company;
            _genericReturnableHelper = genericReturnableHelper;
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
        public async Task<IActionResult> CreateBusiness(List<Business> businesses)
        {
            var result = await _company.CreateCompanyAsync(businesses);

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }

        /// <summary>
        /// Make a paginated query of the results of the companies.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Query(QueryCompanyParams query, string userId)
        {
            var result = await _company.GetBusinesses(query, userId);

            return _genericReturnableHelper.GenericReturnableObject(System.Net.HttpStatusCode.OK, result);
        }
    }
}
