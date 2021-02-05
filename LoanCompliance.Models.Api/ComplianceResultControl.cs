using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCompliance.Models.Api
{
    public class ComplianceResultControl : ComplianceResult
    {
        public bool Skip { get; set; }
    }
}
