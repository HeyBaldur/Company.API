using Company.Common.Connection.v1;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Models.v1;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Repositories
{
    public class CashFlowRepository: ICashFlowRepository
    {
        private readonly IMongoCollection<DiscountedCashFlow> _dcfCollection;

        public CashFlowRepository(
            IOptions<DatabaseSettings> dbSettings
            )
        {
            _dcfCollection = MongoInitializer.Init<DiscountedCashFlow>(dbSettings, nameof(DiscountedCashFlow));
        }

        /// <summary>
        /// Insert one record
        /// </summary>
        /// <param name="cashFlow"></param>
        /// <returns></returns>
        public async Task InsertOneAsync(DiscountedCashFlow cashFlow) => 
            await _dcfCollection.InsertOneAsync(cashFlow);

        /// <summary>
        /// Get one record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DiscountedCashFlow> GetOneAsync(string id)
        {
            var response = await _dcfCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } });

            return response.SingleAsync().Result;
        }
    }
}
