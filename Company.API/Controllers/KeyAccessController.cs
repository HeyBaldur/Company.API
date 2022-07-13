using Company.API.Helpers;
using Company.Infrastructure.Services;
using Company.Models.Dtos;
using Company.Models.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyAccessController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<KeyAccessController> _logger;
        protected readonly IGenericReturnableHelper _genericReturnableHelper;

        public KeyAccessController(
            UserService userService, 
            ILogger<KeyAccessController> logger,
            IGenericReturnableHelper genericReturnableHelper)
        {
            _userService = userService;
            _logger = logger;
            _genericReturnableHelper = genericReturnableHelper;
        }

        /// <summary>
        /// Get secret id and client key. When a user creates an account, the first step is to create client id and secret id.
        /// After these two are created the user can sign up with such credentials and create a password to create a token. 
        /// The token will be required every POST, DELETE, UPDATE or READ.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCredentialsAsync(UserDto userDto)
        {
            _logger.LogInformation($"Creating a new key for {userDto.Email}");

            var result = await _userService.CreateOneAsync(userDto);

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }
    }
}
