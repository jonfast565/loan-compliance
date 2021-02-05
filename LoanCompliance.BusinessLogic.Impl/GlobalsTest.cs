using System.Collections.Generic;
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

        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            var globals = _dataAccess.GetGlobalRulesetData();
            var applicableGlobalRule = globals.FirstOrDefault(x => x.State == query.State
                                                                   && x.ApplicableLoanType == query.LoanType);

            if (applicableGlobalRule == null)
            {
                return new ComplianceResult($"Loan in state {query.State}, type {query.LoanType} does not require compliance testing") {Skip = true};
            }

            if (applicableGlobalRule.MaximumLoanAmount >= query.LoanAmount) return new ComplianceResult();
                return new ComplianceResult($"Maximum loan amount is {applicableGlobalRule.MaximumLoanAmount} for this state, yours is {query.LoanAmount}. " +
                                        "This amount is not eligible for compliance testing.") {Skip = true};
        }
    }
}