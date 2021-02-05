using System.Linq;
using LoanCompliance.Data;
using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class GlobalsTest : IComplianceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public GlobalsTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool ContinueOnFailure { get; set; } = false;

        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            var globals = _dataAccess.GetGlobalRulesetData();
            var applicableGlobalRule = globals.FirstOrDefault(x => x.State == query.State
                                                                   && x.ApplicableLoanType == query.LoanType);

            if (applicableGlobalRule == null)
                return new ComplianceResult("GlobalsTest", true,
                        $"A {query.LoanType} loan in state {query.State} does not require compliance testing.");

            if (applicableGlobalRule.MaximumLoanAmount >= query.LoanAmount)
                return new ComplianceResult("GlobalsTest", true, "Passed");

            return new ComplianceResult("GlobalsTest", false,
                $"The maximum loan amount is ${applicableGlobalRule.MaximumLoanAmount} for {query.State}. Yours is ${query.LoanAmount}. " +
                "This amount is not eligible for compliance testing.");
        }
    }
}