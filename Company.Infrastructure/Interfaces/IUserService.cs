using Company.Infrastructure.Helpers;
using Company.Models.Dtos;
using Company.Models.v1;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<GenericOperationResponse<User>> CreateOneAsync(UserDto userDto);
    }
}
