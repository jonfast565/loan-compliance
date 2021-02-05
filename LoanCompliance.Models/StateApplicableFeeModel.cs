using LoanCompliance.Models.Enums;

namespace LoanCompliance.Models.Data
{
    public class StateApplicableFeeModel
    {
        public State State { get; set; }
        public LoanFeeType LoanFeeType { get; set; }
    }
}