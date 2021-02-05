using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class ValidationTest : IComplianceProcessor
    {
        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            if (query.AnnualPercentageRate < 0 || query.AnnualPercentageRate > 100)
                return new ComplianceResult(
                    new TestResult("ValidationTest", false, "APR not between 0 and 100")) {Skip = true};

            if (query.LoanAmount < 0) return new ComplianceResult(
                new TestResult("ValidationTest", false, "Loan amount is less than 0")) {Skip = true};

            return new ComplianceResult(new TestResult("ValidationTest", true, "Passed"));
        }
    }
}