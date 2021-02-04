using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanConformance.Data;
using LoanConformance.Models.Query;

namespace LoanConformance.BusinessLogic.Impl
{
    public class FeeTest : IConformanceProcessor
    {
        public bool ChecksCompliance { get; set; } = false;

        private IDataAccess _dataAccess;

        public FeeTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceData(ConformanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
