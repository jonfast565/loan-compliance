using System;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class FeeTest : IConformanceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public FeeTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            var fees = _dataAccess.GetFeeData();
            return new ConformanceResult();
        }
    }
}