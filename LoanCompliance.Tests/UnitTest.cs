using System.Collections.Generic;
using LoanCompliance.BusinessLogic.Impl;
using LoanCompliance.Data.InMemory.Impl;
using LoanCompliance.Models.Api;
using LoanCompliance.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCompliance.Tests
{
    [TestClass]
    public class UnitTest
    {
        [DataTestMethod]
        public void TestValidValidLoanComplianceInputs(decimal loanAmount, decimal apr, State state, LoanType loanType, LoanOccupancyType occupancyType, List<LoanFeeAllocation> feeAllocations)
        {
            var processed = QuickBuild(loanAmount, apr, state, loanType, occupancyType, feeAllocations);
            Assert.AreEqual(processed.Success, true);
        }

        [DataTestMethod]
        public void TestInvalidLoanComplianceInputs(decimal loanAmount, decimal apr, State state, LoanType loanType, LoanOccupancyType occupancyType, List<LoanFeeAllocation> feeAllocations)
        {
            var processed = QuickBuild(loanAmount, apr, state, loanType, occupancyType, feeAllocations);
            Assert.AreEqual(processed.Success, false);
        }

        private static ComplianceResult QuickBuild(decimal loanAmount, decimal apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, List<LoanFeeAllocation> feeAllocations)
        {
            var query = new ComplianceQuery
            {
                LoanAmount = loanAmount,
                AnnualPercentageRate = apr,
                State = state,
                LoanType = loanType,
                OccupancyType = occupancyType,
                FeeAllocations = feeAllocations
            };
            var dataAccess = new InMemoryDataAccess();
            var apiFacade = new ApiFacade(dataAccess);
            var processed = apiFacade.ProcessConformanceStep(query);
            return processed;
        }
    }
}