using System;
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

        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            var complianceResult = new ComplianceResult();
            complianceResult = _processors
                .Aggregate(complianceResult,
                    (current, check) =>
                        current + check.ProcessConformanceStep(query));
            return complianceResult;
        }
    }
}