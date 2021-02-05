using System;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class AprTest : IConformanceProcessor
    {
        private IDataAccess _dataAccess;

        public AprTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}