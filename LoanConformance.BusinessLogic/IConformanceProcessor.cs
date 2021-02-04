using LoanConformance.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanConformance.BusinessLogic
{
    public interface IConformanceProcessor
    {
        bool ChecksCompliance { get; set; }
        ConformanceResult ProcessConformanceData(ConformanceQuery query);
    }
}
