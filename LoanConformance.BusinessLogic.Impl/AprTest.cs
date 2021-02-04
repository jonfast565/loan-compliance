using LoanConformance.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanConformance.Data;

namespace LoanConformance.BusinessLogic.Impl
{
    public class AprTest : IConformanceProcessor
    {
        public bool ChecksCompliance { get; set; } = false;

        private IDataAccess _dataAccess;

        public AprTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceData(ConformanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
