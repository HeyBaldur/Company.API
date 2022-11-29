using Company.Models.v1;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Interfaces
{
    public interface ICashFlowRepository
    {
        Task InsertOneAsync(DiscountedCashFlow cashFlow);
        Task<DiscountedCashFlow> GetOneAsync(string id);
    }
}
