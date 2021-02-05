using LoanCompliance.Data;
using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class FeeTest : IComplianceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public FeeTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            var fees = _dataAccess.GetFeeData();
            return new ComplianceResult();
        }
    }
}