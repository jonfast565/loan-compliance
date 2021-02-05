using LoanConformance.Models.Enums;

namespace LoanConformance.Models.Data
{
    public class StateFeeRangeModel
    {
        public State State { get; set; }
        public decimal LowerValue { get; set; }
        public decimal UpperValue { get; set; }
        public decimal PercentageCharged { get; set; }
    }
}