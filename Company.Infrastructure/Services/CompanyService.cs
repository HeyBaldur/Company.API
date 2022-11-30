using AutoMapper;
using Company.Common.Connection.v1;
using Company.Common.Core;
using Company.Common.Helpers;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Models.Dtos;
using Company.Models.Helpers;
using Company.Models.v1;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class CompanyService : ICompany
    {
        private readonly IMongoCollection<Business> _businessCollection;
        private readonly IMapper _mapper;
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

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
        public async Task<GenericOperationResponse<bool>> CreateCompanyAsync(List<BusinessDto> businesses, string userId)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                var businessMapped = _mapper.Map<List<Business>>(businesses);

                foreach (var item in businessMapped)
                {
                    item.UserId = userId;

                    if (item.People.Any())
                    {
                        foreach (var person in item.People)
                        {
                            person.Id = ObjectId.GenerateNewId().ToString();
                            person.UserId = userId;
                        }
                    }
                }

                await _businessCollection.InsertManyAsync(businessMapped);

                _semaphoreSlim.Release();

                return new GenericOperationResponse<bool>(false, Constants.Success, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _semaphoreSlim.Release();

                return new GenericOperationResponse<bool>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete one record from the db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<bool>> DeleteAsync(string id, string userId)
        {
            try
            {
                var result = await _businessCollection.FindOneAndDeleteAsync(x => x.Id == id && x.UserId == userId);

                return new GenericOperationResponse<bool>(false, $"{result.Id} has been deleted", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new GenericOperationResponse<bool>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get business information based on the query params we show
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<PagedList<Business>>> GetBusinesses(QueryCompanyParams query, string userId)
        {

            var cursor = await _businessCollection.FindAsync(x => x.UserId == userId);

            var result = cursor.ToListAsync();

            var businesses = result.Result;

            if (!businesses.Any())
            {
                return new GenericOperationResponse<PagedList<Business>>(true, Constants.NotFound, HttpStatusCode.NotFound);
            }

            var paged = PagedList<Business>.Create(businesses, query.PageNumber, query.PageSize);

            return new GenericOperationResponse<PagedList<Business>>(paged, Constants.Success, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get one record from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Business> GetOneAsync(string id)
        {
            var cursor = await _businessCollection.FindAsync(x => x.Id == id).ConfigureAwait(false);

            return cursor.FirstOrDefault();
        }
    }
}
