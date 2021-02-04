using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanConformance.Data;
using LoanConformance.Models.Query;

namespace LoanConformance.BusinessLogic.Impl
{
    public class GlobalsTest : IConformanceProcessor
    {
        public bool ChecksCompliance { get; set; } = true;

        private IDataAccess _dataAccess;

        public GlobalsTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ConformanceResult ProcessConformanceData(ConformanceQuery query)
        {
            var globals = _dataAccess.GetGlobalRuleset();
            var applicableGlobalRule = globals.FirstOrDefault(x => x.State == query.State
                                        && x.ApplicableLoanType == query.LoanType
                                        && x.MaximumLoanAmount <= query.LoanAmount);
            if (applicableGlobalRule == null)
            {
                return new ConformanceResult
                {
                    ComplianceCheckNeeded = false,
                    Complies = true,
                    FailureReasons =
                    {
                        $"Loan in state {query.State}, type {query.LoanType} does not require compliance testing"
                    }
                };
            }

            return new ConformanceResult
            {
                ComplianceCheckNeeded = true,
                Complies = true,
                FailureReasons = {}
            };
        }
    }
}
