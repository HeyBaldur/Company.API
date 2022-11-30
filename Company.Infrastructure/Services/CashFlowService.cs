using Company.Common.Connection.v1;
using Company.Common.Core;
using Company.Infrastructure.Helpers;
using Company.Infrastructure.Interfaces;
using Company.Infrastructure.Repositories;
using Company.Models.v1;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Services
{
    public class CashFlowService: ICashFlowService
    {
        private readonly CashFlowRepository _cashFlowRepository;

        public CashFlowService(CashFlowRepository cashFlowRepository)
        {
            _cashFlowRepository = cashFlowRepository;
        }

        /// <summary>
        /// Create a new DCF
        /// </summary>
        /// <param name="cashFlow"></param>
        /// <returns></returns>
        public async Task<GenericOperationResponse<DiscountedCashFlow>> SaveDCF(DiscountedCashFlow cashFlow)
        {
            try
            {
                await _cashFlowRepository.InsertOneAsync(cashFlow);

                return new GenericOperationResponse<DiscountedCashFlow>(cashFlow, Constants.Success, HttpStatusCode.OK);
            }
            catch (MongoException mongoEx)
            {
                return new GenericOperationResponse<DiscountedCashFlow>(true, mongoEx.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<GenericOperationResponse<double>> GetDFCCalculation(string[] ids, string uid, double? n = 0, DiscountedCashFlow? cashFlow = null)
        {
            List<DiscountedCashFlow> dfcList = new List<DiscountedCashFlow>();

            double[] calculations = new double[ids.Length];

            await GetRecords(ids, dfcList);

            RunCalculationFromDB(dfcList, calculations);

            ValidateAndCalculateExtraInfo(n, cashFlow, dfcList, calculations);

            if (calculations.Length > 0)
            {
                var calc = calculations.Select(x => (double)x).Sum();

                return new GenericOperationResponse<double>(calc, Constants.Success, HttpStatusCode.OK);
            }

            return new GenericOperationResponse<double>(true, Constants.NotFound, HttpStatusCode.BadRequest);
        }

        private static void ValidateAndCalculateExtraInfo(double? n, DiscountedCashFlow cashFlow, List<DiscountedCashFlow> dfcList, double[] calculations)
        {
            if (n.HasValue && cashFlow != null)
            {
                dfcList.Add(cashFlow);

                double result = (cashFlow.CashFlow) / Math.Pow((1 + cashFlow.DiscountRate), n.Value);

                calculations.Append(result);
            }
        }

        private static void RunCalculationFromDB(List<DiscountedCashFlow> dfcList, double[] calculations)
        {
            for (int i = 0; i < dfcList.Count; i++)
            {
                double result = (dfcList[i].CashFlow) / Math.Pow((1 + dfcList[i].DiscountRate), i + 1);

                calculations[i] = result;
            }
        }

        private async Task GetRecords(string[] ids, List<DiscountedCashFlow> dfcList)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var dfcFromDb = await _cashFlowRepository.GetOneAsync(ids[i]);

                dfcList.Add(dfcFromDb);
            }
        }
    }
}
