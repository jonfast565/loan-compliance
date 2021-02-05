﻿using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class ValidationTest : IComplianceProcessor
    {
        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            if (query.AnnualPercentageRate < 0 || query.AnnualPercentageRate > 100)
                return new ComplianceResult("APR not between 0 and 100") { Skip = true };

            return new ComplianceResult();
        }
    }
}