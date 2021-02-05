using System.Collections.Generic;
using System.Linq;
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
        [DataRow(100000, 6, State.NewYork, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 8, State.NewYork, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]

        [DataRow(100000, 5, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 8, State.Virginia, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.Virginia, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 8, State.Virginia, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.Virginia, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 8, State.Virginia, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]

        [DataRow(100000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 4, State.Maryland, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.Maryland, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 4, State.Maryland, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.Maryland, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]

        [DataRow(100000, 5, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.California, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 3, State.California, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 3, State.California, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.California, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.California, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]
        public void TestValidAprLoanComplianceInputs(decimal loanAmount, decimal apr, State state, LoanType loanType, LoanOccupancyType occupancyType, params LoanFeeAllocation[] allocations)
        {
            var processed = BuildAndRunComplianceTest(loanAmount, apr, state, loanType, occupancyType, allocations.ToList());
            Assert.AreEqual(processed.Success, true);
        }

        [DataTestMethod]
        public void TestInvalidLoanComplianceInputs(decimal loanAmount, decimal apr, State state, LoanType loanType, LoanOccupancyType occupancyType, params LoanFeeAllocation[] allocations)
        {
            var processed = BuildAndRunComplianceTest(loanAmount, apr, state, loanType, occupancyType, allocations.ToList());
            Assert.AreEqual(processed.Success, false);
        }

        private static ComplianceResult BuildAndRunComplianceTest(decimal loanAmount, decimal apr, State state, LoanType loanType,
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
            var processed = apiFacade.ProcessComplianceStep(query);
            return processed;
        }
    }
}