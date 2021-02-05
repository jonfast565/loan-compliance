using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic
{
    public interface IComplianceProcessor
    {
        ComplianceResult ProcessComplianceStep(ComplianceQuery query);
    }
}