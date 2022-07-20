using AutoMapper;
using Company.Common.Connection.v1;
using Company.Common.Core;
using Company.Common.Helpers;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Models.Helpers;
using Company.Models.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class CompanyService : ICompany
    {
        private readonly IMongoCollection<Business> _businessCollection;
        private readonly IMapper _mapper;

        public CompanyService(
            IMapper mapper,
            IOptions<DatabaseSettings> dbSettings)
        {
            _businessCollection = MongoInitializer.Init<Business>(dbSettings, nameof(Business));
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new business. This route allows you to create multiple companies at the same time.
        /// This method allows to create multiple companies at the same time
        /// </summary>
        /// <param name="businesses"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<bool>> CreateCompanyAsync(List<Business> businesses)
        {
            try
            {
                int counter = 0;
                foreach (var business in businesses)
                {
                    await _businessCollection.InsertOneAsync(business);
                    counter++;
                }

                if (counter > 0)
                {
                    return new GenericOperationResponse<bool>(false, Constants.Success, HttpStatusCode.OK);
                }

                return new GenericOperationResponse<bool>(true, @"No information was saved", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return new GenericOperationResponse<bool>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PagedList<Business>> GetBusinesses(QueryCompanyParams query, string userId)
        {

            var cursor = await _businessCollection.FindAsync(x => x.UserId == userId);

            var result = cursor.ToListAsync();

            return await PagedList<Business>.CreateAsync(result.Result, query.PageNumber, query.PageSize);
        }
    }
}
