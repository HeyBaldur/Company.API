using AutoMapper;
using Company.Common.Connection.v1;
using Company.Common.Core;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Models.Dtos;
using Company.Models.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dbSettings"></param>
        public UserService(
            IMapper mapper,
            IOptions<DatabaseSettings> dbSettings
            )
        {
            _mapper = mapper;
            _userCollection = MongoInitializer.Init<User>(dbSettings, nameof(User));
        }

        /// <summary>
        /// Create one user with credentials
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<User>> CreateOneAsync(UserDto userDto)
        {
            // Represents a lightweight alternative to Semaphore that limits the number of threads
            // that can access a resource or pool of resources concurrently.
            await _semaphoreSlim.WaitAsync();

            try
            {
                // Validate if the user exists
                if (await EmailExistsAsync(userDto.Email).ConfigureAwait(false))
                {
                    _semaphoreSlim.Release();
                    return new GenericOperationResponse<User>(true, Constants.UserExists, HttpStatusCode.BadRequest);
                }

                // Validate if the required params are correct
                if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.FullName))
                {
                    _semaphoreSlim.Release();
                    return new GenericOperationResponse<User>(true, Constants.MissingInformation, HttpStatusCode.BadRequest);
                }

                var user = _mapper.Map<User>(userDto);

                user.SecretId = Guid.NewGuid().ToString();
                user.ClientKey = Guid.NewGuid().ToString();
                await _userCollection.InsertOneAsync(user);

                _semaphoreSlim.Release();

                return new GenericOperationResponse<User>(user, Constants.KeyCreated, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Release the semaphore
                _semaphoreSlim.Release();

                return new GenericOperationResponse<User>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get one user record
        /// </summary>
        /// <param name="secretId"></param>
        /// <param name="clientKey"></param>
        /// <returns></returns>
        public async Task<IAsyncCursor<User>> GetOneAsync(string secretId, string clientKey, string email = null)
        {
            if (email == null)
            {
                return await _userCollection.FindAsync(x => x.SecretId == secretId && x.ClientKey == clientKey).ConfigureAwait(false);
            }
            else
            {
                return await _userCollection.FindAsync(x => x.SecretId == secretId && x.ClientKey == clientKey && x.Email == email).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Update a document in mongo db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOneAsync(string id, User user)
        {
            // Represents a lightweight alternative to Semaphore that limits the number of threads
            // that can access a resource or pool of resources concurrently.
            await _semaphoreSlim.WaitAsync();

            try
            {
                var result = await _userCollection.ReplaceOneAsync(x => x.Id == id, user);

                return result.IsAcknowledged;
            }
            finally
            {
                // Represents a lightweight alternative to Semaphore that limits the number of threads
                // that can access a resource or pool of resources concurrently.
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Validate if the user exists in the current context
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<bool> EmailExistsAsync(string email)
        {
            var cursor = await _userCollection.FindAsync(x => x.Email == email).ConfigureAwait(false);

            var user = await cursor.FirstOrDefaultAsync();

            return user == null ? false : true;
        }
    }
}
