using System.Linq;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class GlobalsTest : IComplianceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public GlobalsTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            var globals = _dataAccess.GetGlobalRulesetData();
            var applicableGlobalRule = globals.FirstOrDefault(x => x.State == query.State
                                                                   && x.ApplicableLoanType == query.LoanType
                                                                   && x.MaximumLoanAmount <= query.LoanAmount);

            if (applicableGlobalRule == null)
                return new ComplianceResult(
                    $"Loan in state {query.State}, type {query.LoanType} does not require compliance testing");

            return new ComplianceResult();
        }
    }
}