using System.Collections.Generic;
using System.Linq;
using LoanCompliance.Data;
using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class ApiFacade : IComplianceProcessor
    {
        private readonly List<IComplianceProcessor> _processors;

        public ApiFacade(IDataAccess dataAccess)
        {
            _processors = new List<IComplianceProcessor>
            {
                new ValidationTest(),
                new GlobalsTest(dataAccess),
                new AprTest(dataAccess),
                new FeeTest(dataAccess)
            };
        }

        public bool ContinueOnFailure { get; set; } = true;

        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            var complianceResult = new ComplianceResult();
            foreach (var step in _processors)
            {
                var stepResult = step.ProcessComplianceStep(query);
                if (!stepResult.Success && !step.ContinueOnFailure)
                {
                    return stepResult + complianceResult;
                }
                complianceResult = stepResult + complianceResult;
            }
            return complianceResult;
        }
    }
}