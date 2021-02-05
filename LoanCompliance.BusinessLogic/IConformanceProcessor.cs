using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic
{
    public interface IConformanceProcessor
    {
        ConformanceResult ProcessConformanceStep(ConformanceQuery query);
    }
}