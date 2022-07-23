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
        private readonly DeveloperService _developerService;
        private readonly ILogger<KeyAccessController> _logger;
        protected readonly IGenericReturnableHelper _genericReturnableHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        /// <param name="genericReturnableHelper"></param>
        /// <param name="developerService"></param>
        public KeyAccessController(
            UserService userService,
            ILogger<KeyAccessController> logger,
            IGenericReturnableHelper genericReturnableHelper,
            DeveloperService developerService)
        {
            _userService = userService;
            _logger = logger;
            _genericReturnableHelper = genericReturnableHelper;
            _developerService = developerService;
        }

        /// <summary>
        /// Get secret id and client key. When a user creates an account, the first step is to create client id and secret id.
        /// After these two are created the user can sign up with such credentials and create a password to create a token. 
        /// The token will be required every POST, DELETE, UPDATE or READ.
        /// </summary>
        /// <param name="user">The user request to create a new account</param>
        /// <returns></returns>
        [HttpPost("Secrets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCredentialsAsync(UserDto user)
        {
            _logger.LogInformation($"Creating a new key for {user.Email}");

            var result = await _userService.CreateOneAsync(user);

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }

        /// <summary>
        /// Create a new developer account. Once the user created a secret and client ids the developer/user
        /// must create a developer account. This account will be used to send TOKENs to authenticate in every endpoint
        /// the developer/user requests.
        /// </summary>
        /// <param name="password">The password to authenticate</param>
        /// <param name="secretId">Secret ID provided when user created the first request</param>
        /// <param name="clientKey">Secret KEY provided when user created the first request</param>
        /// <returns></returns>
        [HttpPost("Account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDeveloperAccount(string password, string secretId, string clientKey)
        {
            var result = await _developerService.CreateOneAsync(password, secretId, clientKey);

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }

        /// <summary>
        /// Generate token to authenticate. This token will be valid for 30 days. Every 30 days the user must update the token or create a new one to 
        /// avoid future issues. If the user/developer does not forget about the token, it is possible to get a new token from this same endpoint.
        /// </summary>
        /// <param name="request">Request of the user to authenticate</param>
        /// <returns></returns>
        [HttpPost("GetToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetToken(AuthRequest request)
        {
            _logger.LogInformation(@"User is requesting token");

            var result = await _developerService.GenerateToken(request);

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, result);
        }
    }
}
