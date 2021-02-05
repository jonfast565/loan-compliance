using LoanCompliance.Models.Enums;

namespace LoanCompliance.Models.Data
{
    public class GlobalRulesetModel
    {
        public State State { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public LoanType ApplicableLoanType { get; set; }
    }
}