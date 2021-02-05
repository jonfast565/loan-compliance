using System.Linq;
using LoanConformance.Data;
using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class GlobalsTest : IConformanceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public GlobalsTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool GlobalCheck { get; set; } = true;

        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            var globals = _dataAccess.GetGlobalRuleset();
            var applicableGlobalRule = globals.FirstOrDefault(x => x.State == query.State
                                                                   && x.ApplicableLoanType == query.LoanType
                                                                   && x.MaximumLoanAmount <= query.LoanAmount);

            if (applicableGlobalRule == null)
                return new ConformanceResult(
                    $"Loan in state {query.State}, type {query.LoanType} does not require compliance testing");

            return new ConformanceResult();
        }
    }
}