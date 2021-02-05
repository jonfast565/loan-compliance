using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic
{
    public interface IComplianceProcessor
    {
        ComplianceResult ProcessConformanceStep(ComplianceQuery query);
    }
}