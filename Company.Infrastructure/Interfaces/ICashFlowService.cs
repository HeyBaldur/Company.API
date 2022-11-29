using Company.Infrastructure.Helpers;
using Company.Models.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface ICashFlowService
    {
        Task<GenericOperationResponse<DiscountedCashFlow>> SaveDCF(DiscountedCashFlow cashFlow);
        Task<GenericOperationResponse<double>> GetDFCCalculation(string[] ids, string uid, double? n = 0, DiscountedCashFlow? cashFlow = null);
    }
}
