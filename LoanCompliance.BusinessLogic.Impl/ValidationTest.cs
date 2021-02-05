using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class ValidationTest : IComplianceProcessor
    {
        public bool ContinueOnFailure { get; set; } = false;

        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            if (query.AnnualPercentageRate < 0 || query.AnnualPercentageRate > 100)
                return new ComplianceResult("ValidationTest", false,
                    $"APR {query.AnnualPercentageRate} not between 0 and 100");

            if (query.LoanAmount < 0)
                return new ComplianceResult("ValidationTest", false, $"Loan amount {query.LoanAmount} is less than 0");

            return new ComplianceResult("ValidationTest", true, "Passed");
        }
    }
}