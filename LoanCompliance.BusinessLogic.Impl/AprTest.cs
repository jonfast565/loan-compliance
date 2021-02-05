using System;
using System.Linq;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class AprTest : IConformanceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public AprTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            var aprData = _dataAccess.GetAprData();
            var normalizedApr = query.AnnualPercentageRate / 100;
            var aprRule = aprData.FirstOrDefault(x =>
                x.LoanType == query.LoanType
                && x.OccupancyType == query.OccupancyType
                && x.State == query.State);

            if (aprRule == null)
            {
                return new ConformanceResult();
            }

            if (aprRule.AnnualRatePercentage > normalizedApr)
            {
                return new ConformanceResult(
                    $"{aprRule.AnnualRatePercentage}% > {normalizedApr}% " +
                    $"maximum for {query.LoanType} in {query.State} " +
                    $"with {query.OccupancyType}");
            }
            return new ConformanceResult();
        }
    }
}