using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic
{
    public interface IComplianceProcessor
    {
        ComplianceResult ProcessConformanceStep(ComplianceQuery query);
    }
}