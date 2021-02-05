using System.Collections.Generic;
using LoanCompliance.Models.Enums;

namespace LoanCompliance.Models.Data
{
    public class StateFeeModel
    {
        public State State { get; set; }
        public LoanFeeType LoanFeeType { get; set; }
        public IEnumerable<StateFeeRangeModel> MaxChargeFeeRanges { get; set; }
    }
}