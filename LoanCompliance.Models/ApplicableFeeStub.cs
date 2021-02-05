using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanCompliance.Models.Enums;

namespace LoanCompliance.Models.Data
{
    public class ApplicableFeeStub
    {
        public decimal FeeCharged { get; set; } 
        public LoanFeeType LoanFeeType { get; set; }
    }
}
