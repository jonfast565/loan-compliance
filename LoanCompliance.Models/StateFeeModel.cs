using System.Collections.Generic;
using LoanConformance.Models.Enums;

namespace LoanConformance.Models.Data
{
    public class StateFeeModel
    {
        public State State { get; set; }
        public LoanFeeType LoanFeeTypes { get; set; }
        public IEnumerable<StateFeeRangeModel> MaxChargeFeeRanges { get; set; }
    }
}