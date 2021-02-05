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

        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            var aprData = _dataAccess.GetAprData();
            var normalizedApr = query.AnnualPercentageRate / 100;
            var aprRule = aprData.FirstOrDefault(x =>
                x.LoanType == query.LoanType
                && x.OccupancyType == query.OccupancyType
                && x.State == query.State);

            if (aprRule == null)
                return new ComplianceResult(
                    new TestResult("AprTest",false,$"APR Test not run for loan type {query.LoanType} " +
                                                  $"with occupancy {query.OccupancyType} " +
                                                  $"in state {query.State}"));

            if (normalizedApr > aprRule.AnnualRatePercentage)
                return new ComplianceResult(
                    new TestResult("AptTest", false,
                    $"The {aprRule.AnnualRatePercentage * 100}% APR > {query.AnnualPercentageRate}% APR " +
                    $"maximum for {query.LoanType} in {query.State} " +
                    $"with {query.OccupancyType}"));
            return new ComplianceResult(new TestResult("AprTest", true, "Passed"));
        }
    }
}