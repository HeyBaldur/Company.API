using Company.Common.Helpers;
using Company.Infrastructure.Helpers;
using Company.Models.Dtos;
using Company.Models.Helpers;
using Company.Models.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface ICompany
    {
        /// <summary>
        /// Create a new list or one company record
        /// </summary>
        /// <param name="businesses"></param>
        /// <returns></returns>
        Task<GenericOperationResponse<bool>> CreateCompanyAsync(List<BusinessDto> businesses, string userId);

        /// <summary>
        /// Get all business based on the params. This is used for pagination
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<GenericOperationResponse<PagedList<Business>>> GetBusinesses(QueryCompanyParams query, string userId);

        /// <summary>
        /// Delelte one business from repo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<GenericOperationResponse<bool>> DeleteAsync(string id, string userId);

    }
}
