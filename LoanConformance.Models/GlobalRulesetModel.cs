namespace LoanConformance.Models.Data
{
    public class GlobalRulesetModel
    {
        public StateEnum State { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public LoanTypeEnum ApplicableLoanType { get; set; }
    }
}
