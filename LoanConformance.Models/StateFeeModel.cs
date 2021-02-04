using System.Collections.Generic;

namespace LoanConformance.Models.Data
{
    public class StateFeeModel
    {
        public StateEnum State { get; set; }
        public LoanFeeTypeEnum LoanFeeTypes { get; set; }
        public IEnumerable<StateFeeRangeModel> MaxChargeFeeRanges { get; set; }
    }
}
