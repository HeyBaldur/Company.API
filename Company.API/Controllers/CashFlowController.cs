using Company.API.Helpers;
using Company.Common.Inerfaces;
using Company.Infrastructure.Services;
using Company.Models.Dtos;
using Company.Models.Helpers;
using Company.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CashFlowController : ControllerBase
    {
        private readonly CashFlowService _cashFlowService;
        protected readonly IGenericReturnableHelper _genericReturnableHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cashFlowService"></param>
        /// <param name="genericReturnableHelper"></param>
        public CashFlowController(
            CashFlowService cashFlowService,
            IGenericReturnableHelper genericReturnableHelper)
        {
            _cashFlowService = cashFlowService;
            _genericReturnableHelper = genericReturnableHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cashFlow"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateCashflow(DiscountedCashFlow cashFlow)
        {
            var result = _cashFlowService.SaveDCF(cashFlow);

            return _genericReturnableHelper.GenericReturnableObject(System.Net.HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dfcParams"></param>
        /// <returns></returns>
        [HttpPost("Calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Calculate(DfcParams dfcParams)
        {
            var result = await _cashFlowService.GetDFCCalculation(dfcParams.ids, dfcParams.uid, dfcParams.N, dfcParams.cashFlow);

            return _genericReturnableHelper.GenericReturnableObject(System.Net.HttpStatusCode.OK, result);
        }
    }
}
 