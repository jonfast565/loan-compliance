using LoanConformance.Models.Api;

namespace LoanConformance.BusinessLogic.Impl
{
    public class ValidationTest : IConformanceProcessor
    {
        public ConformanceResult ProcessConformanceStep(ConformanceQuery query)
        {
            if (query.AnnualPercentageRate < 0 || query.AnnualPercentageRate > 100)
                return new ConformanceResult("APR not between 0 and 100");

            return new ConformanceResult();
        }
    }
}