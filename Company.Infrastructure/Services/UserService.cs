using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Company.Models.v1;
using Company.Common.Connection.v1;
using Microsoft.Extensions.Options;
using Company.Infrastructure.Interfaces;
using Company.Models.Dtos;
using Company.Infrastructure.Helpers;
using System.Net;

namespace Company.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(
            IOptions<DatabaseSettings> messageServiceDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                messageServiceDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                messageServiceDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                messageServiceDatabaseSettings.Value.UserCollectionName);
        }

        /// <summary>
        /// Create one user with credentials
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<User>> CreateOneAsync(UserDto userDto)
        {
            try
            {
                var user = new User
                {
                    ClientKey = Guid.NewGuid().ToString(),
                    SecretId = Guid.NewGuid().ToString(),
                    Email = userDto.Email,
                    FullName = userDto.FullName,
                    IsActive = false
                };

                await _userCollection.InsertOneAsync(user);

                return new GenericOperationResponse<User>(user, @"Success", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new GenericOperationResponse<User>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
