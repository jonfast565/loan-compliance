using System;
using System.Collections.Generic;
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

        public ComplianceResult ProcessConformanceStep(ComplianceQuery query)
        {
            var fees = _dataAccess.GetFeeData();

            var applicableFees =
                (from allocations in query.FeeAllocations
                join feeRecords in fees
                    on new {allocations.LoanFeeType, query.State}
                    equals new {feeRecords.LoanFeeType, feeRecords.State}
                select new FeeJoiner
                {
                    FeeCharged = allocations.FeeCharged,
                    LoanFeeType = allocations.LoanFeeType,
                    MaxChargeFeeRanges = feeRecords.MaxChargeFeeRanges
                }).ToList();

            var issues = new List<string>();
            var totalFees = applicableFees.Sum(applicableFee => applicableFee.FeeCharged);
            var feeLimit = applicableFees.SelectMany(x => x.MaxChargeFeeRanges);

            // TODO: BROKEN, data model is messed up, must finish tomorrow.

            return issues.Any() ? new ComplianceResult(issues) : new ComplianceResult();
        }
    }
}