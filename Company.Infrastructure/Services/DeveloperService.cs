using AutoMapper;
using Company.Common.Connection.v1;
using Company.Common.Core;
using Company.Common.Helpers;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Models.v1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IMongoCollection<Developer> _developerCollection;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly IConfiguration _iConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dbSettings"></param>
        /// <param name="userService"></param>
        public DeveloperService(
            IMapper mapper,
            IOptions<DatabaseSettings> dbSettings,
            UserService userService,
            IConfiguration iConfiguration)
        {
            _mapper = mapper;
            _userService = userService;
            _developerCollection = MongoInitializer.Init<Developer>(dbSettings, nameof(Developer));
            _iConfiguration = iConfiguration;
        }

        /// <summary>
        /// Create a developer account based on the secret, client and password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="secretId"></param>
        /// <param name="clientKey"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<User>> CreateOneAsync(string password, string secretId, string clientKey)
        {
            try
            {
                var user = await GetUserAsync(secretId, clientKey);

                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(secretId) || string.IsNullOrEmpty(clientKey))
                {
                    return new GenericOperationResponse<User>(true, Constants.MissingInformation, HttpStatusCode.BadRequest);
                }

                if (user != null && !user.IsActive)
                {
                    PasswordHash.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                    Developer developer = new Developer
                    {
                        ClientKey = clientKey,
                        SecretId = secretId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    await _developerCollection.InsertOneAsync(developer);

                    var updateResult = await UpdateCurrentUser(user, _userService, user.Id).ConfigureAwait(false);

                    return new GenericOperationResponse<User>(user, Constants.DeveloperCreated, HttpStatusCode.OK);
                }
                else
                {
                    return new GenericOperationResponse<User>(true, "User does not exist", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new GenericOperationResponse<User>(true, ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Generate token for authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<string>> GenerateToken(AuthRequest request)
        {
            try
            {
                // Cleaning request and remove all threats from possible attacks
                // Cross-Site Scripting (XSS) attacks are a type of injection,
                // in which malicious scripts are injected into otherwise benign and trusted websites
                // Check the following links for more information
                // => https://github.com/mganss/HtmlSanitizer
                // => https://owasp.org/www-community/attacks/xss/
                AuthRequest authRequestClean = new AuthRequest()
                {
                    ClientKey = ApiSanitizer.Sanitizer(request.ClientKey),
                    Email = ApiSanitizer.Sanitizer(request.Email),
                    Password = ApiSanitizer.Sanitizer(request.Password),
                    SecretId = ApiSanitizer.Sanitizer(request.SecretId)
                };

                var user = await GetUserAsync(authRequestClean.SecretId, authRequestClean.ClientKey, authRequestClean.Email);

                var developer = await GetDeveloperAsync(authRequestClean.SecretId, authRequestClean.ClientKey);

                var passHasher = new Helpers.PasswordHash();

                if (user != null && developer != null && (developer.PasswordHash != null || developer.PasswordSalt != null))
                {
                    if (!passHasher.VerifyPassword(authRequestClean.Password, developer.PasswordHash, developer.PasswordSalt))
                    {
                        return new GenericOperationResponse<string>(true, "Wrong credentials", HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        var token = SetTokenStrategy(user.Id, user.Email, developer.SecretId, developer.ClientKey);
                        return new GenericOperationResponse<string>(token, Constants.Success, HttpStatusCode.OK);
                    }
                }
                else
                {
                    return new GenericOperationResponse<string>(true, "Use a different authentication method", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new GenericOperationResponse<string>(true, ex.Message.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Set token
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="email"></param>
        /// <param name="secret"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        private string SetTokenStrategy(string uid, string email, string secret, string client)
        {
            return GetToken(uid, email, secret, client);
        }

        /// <summary>
        /// Get token to authenticate
        /// Token will be valid for 30 days
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="email"></param>
        /// <param name="secret"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string GetToken(string uid, string email, string secret, string client)
        {
            // Set claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, uid),
                new Claim(ClaimTypes.Email, email)
            };

            // Set key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenValue = tokenHandler.WriteToken(token);

            return tokenValue;
        }

        /// <summary>
        /// Update current user with the active user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userService"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        private async Task<bool> UpdateCurrentUser(User user, UserService userService, string uid)
        {
            user.IsActive = true;
            return await userService.UpdateOneAsync(uid, user).ConfigureAwait(false);
        }

        /// <summary>
        /// Get current user by secret and client
        /// </summary>
        /// <param name="secretId"></param>
        /// <param name="clientKey"></param>
        /// <returns></returns>
        private async Task<User> GetUserAsync(string secretId, string clientKey, string email = null)
        {
            var cursorUser = await _userService.GetOneAsync(secretId, clientKey, email);

            var user = cursorUser.FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Return a developer object from data context
        /// </summary>
        /// <param name="secretId"></param>
        /// <param name="clientKey"></param>
        /// <returns></returns>
        private async Task<Developer> GetDeveloperAsync(string secretId, string clientKey)
        {
            var cursorUser = await _developerCollection.FindAsync(x => x.SecretId == secretId && x.ClientKey == clientKey);

            return cursorUser.FirstOrDefault();
        }
    }
}
