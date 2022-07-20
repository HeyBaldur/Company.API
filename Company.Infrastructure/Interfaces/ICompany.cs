using Company.Common.Helpers;
using Company.Infrastructure.Helpers;
using Company.Models.Helpers;
using Company.Models.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface ICompany
    {
        Task<GenericOperationResponse<bool>> CreateCompanyAsync(List<Business> businesses);
        Task<PagedList<Business>> GetBusinesses(QueryCompanyParams query, string userId);
    }
}
