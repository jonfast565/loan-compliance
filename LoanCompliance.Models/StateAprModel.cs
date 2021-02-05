using LoanConformance.Models.Enums;

namespace LoanConformance.Models.Data
{
    public class StateAprModel
    {
        public StateEnum State { get; set; }
        public LoanTypeEnum LoanType { get; set; }
        public LoanOccupancyTypeEnum OccupancyType { get; set; }
        public decimal AnnualRatePercentage { get; set; }
    }
}