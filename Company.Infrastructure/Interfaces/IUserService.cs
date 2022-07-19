using Company.Infrastructure.Helpers;
using Company.Models.Dtos;
using Company.Models.v1;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<GenericOperationResponse<User>> CreateOneAsync(UserDto userDto);
        Task<IAsyncCursor<User>> GetOneAsync(string secretId, string clientKey, string email);
        Task<bool> UpdateOneAsync(string id, User user);
    }
}
