using System.Linq;
using LoanCompliance.Data;
using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class AprTest : IComplianceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public AprTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            var aprData = _dataAccess.GetAprData();
            var normalizedApr = query.AnnualPercentageRate / 100;
            var aprRule = aprData.First(x =>
                x.LoanType == query.LoanType
                && x.OccupancyType == query.OccupancyType
                && x.State == query.State);

            if (aprRule.AnnualRatePercentage > normalizedApr)
                return new ComplianceResult(
                    $"{aprRule.AnnualRatePercentage}% > {normalizedApr}% " +
                    $"maximum for {query.LoanType} in {query.State} " +
                    $"with {query.OccupancyType}");
            return new ComplianceResult();
        }
    }
}