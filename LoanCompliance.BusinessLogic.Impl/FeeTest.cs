using System.Linq;
using LoanCompliance.Data;
using LoanCompliance.Models.Api;
using LoanCompliance.Models.Data;

namespace LoanCompliance.BusinessLogic.Impl
{
    public class FeeTest : IComplianceProcessor
    {
        private readonly IDataAccess _dataAccess;

        public FeeTest(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool ContinueOnFailure { get; set; } = true;

        /// <summary>
        /// Processes validations of the loan fees, only works on fees applicable to a state
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ComplianceResult ProcessComplianceStep(ComplianceQuery query)
        {
            var applicableFeeData = _dataAccess.GetApplicableFeeData();
            var feeRanges = _dataAccess.GetFeeRangeData();

            var applicableFees =
                (from allocations in query.FeeAllocations
                    join feeRecords in applicableFeeData
                        on new {allocations.LoanFeeType, query.State}
                        equals new {feeRecords.LoanFeeType, feeRecords.State}
                    select new ApplicableFeeStub
                    {
                        FeeCharged = allocations.FeeCharged,
                        LoanFeeType = allocations.LoanFeeType
                    }).ToList();

            if (!applicableFees.Any())
                return new ComplianceResult("FeeTest", true,
                    $"No applicable fees for this loan in state {query.State}, test not run");

            var totalApplicableFees = applicableFees.Sum(applicableFee => applicableFee.FeeCharged);
            var feeRange = feeRanges.First(x => x.State == query.State
                                                && query.LoanAmount <= x.UpperValue &&
                                                query.LoanAmount > x.LowerValue);

            return totalApplicableFees > feeRange.PercentageCharged * query.LoanAmount
                ? new ComplianceResult("FeesTest", false,
                    $"The applicable fees charged ${totalApplicableFees} is greater than {feeRange.PercentageCharged * 100}% of the total loan amount in {query.State}.",
                    $"Loan amount of ${query.LoanAmount} at {feeRange.PercentageCharged * 100}% = ${feeRange.PercentageCharged * query.LoanAmount}."
                )
                : new ComplianceResult("FeesTest", true, "Passed");
        }
    }
}