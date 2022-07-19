using Company.Infrastructure.Helpers;
using Company.Models.v1;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface IDeveloperService
    {
        Task<GenericOperationResponse<User>> CreateOneAsync(string password, string secretId, string clientKey);
        Task<GenericOperationResponse<string>> GenerateToken(AuthRequest request);
    }
}
