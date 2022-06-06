using Company.API.Helpers;
using Company.Infrastructure.Services;
using Company.Models.Dtos;
using Company.Models.v1;
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
        private readonly IGenericReturnableHelper _genericReturnableHelper;

        public KeyAccessController(
            UserService userService, 
            ILogger<KeyAccessController> logger,
            IGenericReturnableHelper genericReturnableHelper)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get secret id and client key
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetCredentialsAsync(UserDto userDto)
        {
            _logger.LogInformation($"Creating a new key for {userDto.Email}");
            var result = await _userService.CreateOneAsync(userDto);

            var credentials = new
            {
                result.Result.ClientKey,
                result.Result.SecretId
            };

            return _genericReturnableHelper.GenericReturnableObject(result.StatusCode, credentials);
        }
    }
}
