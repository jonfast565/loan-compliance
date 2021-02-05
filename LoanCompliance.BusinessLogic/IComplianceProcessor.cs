using LoanCompliance.Models.Api;

namespace LoanCompliance.BusinessLogic
{
    public interface IComplianceProcessor
    {
        public bool ContinueOnFailure { get; set; }
        ComplianceResult ProcessComplianceStep(ComplianceQuery query);
    }
}