using LoanConformance.Models.Enums;

namespace LoanConformance.Models.Data
{
    public class StateAprModel
    {
        public State State { get; set; }
        public LoanType LoanType { get; set; }
        public LoanOccupancyType OccupancyType { get; set; }
        public decimal AnnualRatePercentage { get; set; }
    }
}