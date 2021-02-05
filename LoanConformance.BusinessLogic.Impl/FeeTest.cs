using System;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class FeeTest : IConformanceProcessor
    {
        private IDataAccess _dataAccess;

        public FeeTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool GlobalCheck { get; set; } = false;

        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}