using System;
using System.Collections.Generic;
using LoanCompliance.BusinessLogic;
using LoanCompliance.BusinessLogic.Impl;
using LoanCompliance.Data;
using LoanCompliance.Data.InMemory.Impl;
using LoanCompliance.Models.Api;
using LoanCompliance.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCompliance.Tests
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///     Test valid APR inputs
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        /// <param name="allocations"></param>
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
        public void TestValidAprLoanComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, params LoanFeeAllocation[] allocations)
        {
            var processed = BuildAprTest(loanAmount, apr, state, loanType, occupancyType);
            PrintReasons(processed);
            Assert.AreEqual(true, processed.Success);
        }

        /// <summary>
        ///     Test invalid APR inputs
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        [DataTestMethod]
        [DataRow(100000, 7, State.NewYork, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 9, State.NewYork, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 9, State.Virginia, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 6, State.Virginia, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 9, State.Virginia, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 6, State.Virginia, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 9, State.Virginia, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 5, State.Maryland, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 5, State.California, LoanType.Conventional, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 4, State.California, LoanType.VA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 4, State.California, LoanType.VA, LoanOccupancyType.SecondaryOccupancy)]
        [DataRow(100000, 6, State.California, LoanType.FHA, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(100000, 5, State.California, LoanType.FHA, LoanOccupancyType.SecondaryOccupancy)]
        public void TestInvalidAprLoanComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType)
        {
            var processed = BuildAprTest(loanAmount, apr, state, loanType, occupancyType);
            PrintReasons(processed);
            Assert.AreEqual(false, processed.Success);
        }

        /// <summary>
        ///     Test valid fee inputs with two fees
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        /// <param name="feeType1"></param>
        /// <param name="feeCharged1"></param>
        /// <param name="feeType2"></param>
        /// <param name="feeCharged2"></param>
        [DataTestMethod]
        [DataRow(50000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 750, LoanFeeType.Settlement, 750)]
        [DataRow(150000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 3000, LoanFeeType.Settlement, 3000)]
        [DataRow(151000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 0, LoanFeeType.Settlement, 0)]
        [DataRow(150000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 3000, LoanFeeType.Settlement, 3000)]
        [DataRow(151000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 0, LoanFeeType.Settlement, 0)]
        public void TestTwoValidFeeComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2)
        {
            var processed = BuildFeesTest2(loanAmount, apr, state, loanType, occupancyType, feeType1, feeCharged1,
                feeType2, feeCharged2);
            PrintReasons(processed);
            Assert.AreEqual(true, processed.Success);
        }

        /// <summary>
        ///     Test valid fee inputs with three fees
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        /// <param name="feeType1"></param>
        /// <param name="feeCharged1"></param>
        /// <param name="feeType2"></param>
        /// <param name="feeCharged2"></param>
        /// <param name="feeType3"></param>
        /// <param name="feeCharged3"></param>
        [DataTestMethod]
        [DataRow(50000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 750, LoanFeeType.Processing, 750, LoanFeeType.Settlement, 10)]
        [DataRow(150000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 3000, LoanFeeType.Processing, 3000, LoanFeeType.Settlement, 10)]
        [DataRow(151000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 0, LoanFeeType.Processing, 0, LoanFeeType.Settlement, 10)]
        public void TestThreeValidFeeComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2, LoanFeeType feeType3, int feeCharged3)
        {
            var processed = BuildFeesTest3(loanAmount, apr, state, loanType, occupancyType, feeType1, feeCharged1,
                feeType2, feeCharged2, feeType3, feeCharged3);
            PrintReasons(processed);
            Assert.AreEqual(true, processed.Success);
        }

        /// <summary>
        ///     Test invalid inputs with two fees
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        /// <param name="feeType1"></param>
        /// <param name="feeCharged1"></param>
        /// <param name="feeType2"></param>
        /// <param name="feeCharged2"></param>
        [DataTestMethod]
        [DataRow(50000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 75044, LoanFeeType.Settlement, 750)]
        [DataRow(150000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 304400, LoanFeeType.Settlement, 3000)]
        [DataRow(151000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 345345, LoanFeeType.Settlement, 0)]
        [DataRow(150000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 304400, LoanFeeType.Settlement, 3000)]
        [DataRow(151000, 4, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.Application, 444440, LoanFeeType.Settlement, 4444440)]
        public void TestTwoInvalidFeeComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2)
        {
            var processed = BuildFeesTest2(loanAmount, apr, state, loanType, occupancyType, feeType1, feeCharged1,
                feeType2, feeCharged2);
            PrintReasons(processed);
            Assert.AreEqual(false, processed.Success);
        }

        /// <summary>
        ///     Test invalid inputs with three fees
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        /// <param name="feeType1"></param>
        /// <param name="feeCharged1"></param>
        /// <param name="feeType2"></param>
        /// <param name="feeCharged2"></param>
        /// <param name="feeType3"></param>
        /// <param name="feeCharged3"></param>
        [DataTestMethod]
        [DataRow(50000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 750000, LoanFeeType.Processing, 750, LoanFeeType.Settlement, 10)]
        [DataRow(150000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 3000, LoanFeeType.Processing, 300000, LoanFeeType.Settlement, 10)]
        [DataRow(151000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy,
            LoanFeeType.FloodCertification, 100888, LoanFeeType.Processing, 0, LoanFeeType.Settlement, 10)]
        public void TestThreeInvalidFeeComplianceInputs(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2, LoanFeeType feeType3, int feeCharged3)
        {
            var processed = BuildFeesTest3(loanAmount, apr, state, loanType, occupancyType, feeType1, feeCharged1,
                feeType2, feeCharged2, feeType3, feeCharged3);
            PrintReasons(processed);
            Assert.AreEqual(false, processed.Success);
        }

        /// <summary>
        ///     Test valid globals
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        [DataTestMethod]
        [DataRow(750000, 6, State.NewYork, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(800000, 6, State.Virginia, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(400000, 6, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(600000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        public void TestValidGlobalParameters(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType)
        {
            var processed = BuildGlobalsTest(loanAmount, apr, state, loanType, occupancyType);
            PrintReasons(processed);
            Assert.AreEqual(true, processed.Success);
        }

        /// <summary>
        ///     Test invalid globals
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="apr"></param>
        /// <param name="state"></param>
        /// <param name="loanType"></param>
        /// <param name="occupancyType"></param>
        [DataTestMethod]
        [DataRow(3750000, 6, State.NewYork, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(3400000, 6, State.Maryland, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        [DataRow(3600000, 6, State.California, LoanType.Conventional, LoanOccupancyType.PrimaryOccupancy)]
        public void TestInvalidGlobalParameters(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType)
        {
            var processed = BuildGlobalsTest(loanAmount, apr, state, loanType, occupancyType);
            PrintReasons(processed);
            Assert.AreEqual(false, processed.Success);
        }

        [TestMethod]
        public void ComprehensiveApplicationHappyPathTest()
        {
            var result = BuildAndRunComplianceTest(100000, 3.8m, State.Virginia,
                LoanType.Conventional,
                LoanOccupancyType.PrimaryOccupancy, new List<LoanFeeAllocation>
                {
                    new() {FeeCharged = 3800, LoanFeeType = LoanFeeType.Processing}
                },
                da => new ApiFacade(da));
            PrintReasons(result);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ComprehensiveApplicationSadPathTest()
        {
            var result = BuildAndRunComplianceTest(100000, 3.8m, State.Virginia,
                LoanType.Conventional,
                LoanOccupancyType.PrimaryOccupancy, new List<LoanFeeAllocation>
                {
                    new() {FeeCharged = 7100, LoanFeeType = LoanFeeType.Processing}
                },
                da => new ApiFacade(da));
            PrintReasons(result);
            Assert.AreEqual(false, result.Success);
        }

        #region Builder Methods

        private static ComplianceResult BuildGlobalsTest(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType)
        {
            return BuildAndRunComplianceTest(Convert.ToDecimal(loanAmount), Convert.ToDecimal(apr), state,
                loanType, occupancyType, new List<LoanFeeAllocation>(), da => new GlobalsTest(da));
        }

        private static ComplianceResult BuildAprTest(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType)
        {
            return BuildAndRunComplianceTest(Convert.ToDecimal(loanAmount), Convert.ToDecimal(apr), state,
                loanType, occupancyType, new List<LoanFeeAllocation>(), da => new AprTest(da));
        }

        private static ComplianceResult BuildFeesTest2(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2)
        {
            return BuildAndRunComplianceTest(Convert.ToDecimal(loanAmount), Convert.ToDecimal(apr), state,
                loanType, occupancyType, new List<LoanFeeAllocation>
                {
                    new() {FeeCharged = Convert.ToDecimal(feeCharged1), LoanFeeType = feeType1},
                    new() {FeeCharged = Convert.ToDecimal(feeCharged2), LoanFeeType = feeType2}
                }, da => new FeeTest(da));
        }

        private static ComplianceResult BuildFeesTest3(int loanAmount, int apr, State state, LoanType loanType,
            LoanOccupancyType occupancyType, LoanFeeType feeType1, int feeCharged1, LoanFeeType feeType2,
            int feeCharged2, LoanFeeType feeType3, int feeCharged3)
        {
            return BuildAndRunComplianceTest(Convert.ToDecimal(loanAmount), Convert.ToDecimal(apr), state,
                loanType, occupancyType, new List<LoanFeeAllocation>
                {
                    new() {FeeCharged = Convert.ToDecimal(feeCharged1), LoanFeeType = feeType1},
                    new() {FeeCharged = Convert.ToDecimal(feeCharged2), LoanFeeType = feeType2},
                    new() {FeeCharged = Convert.ToDecimal(feeCharged3), LoanFeeType = feeType3}
                }, da => new FeeTest(da));
        }

        private static ComplianceResult BuildAndRunComplianceTest(decimal loanAmount, decimal apr, State state,
            LoanType loanType,
            LoanOccupancyType occupancyType, List<LoanFeeAllocation> feeAllocations,
            Func<IDataAccess, IComplianceProcessor> processorBuilder)
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
            var processor = processorBuilder(dataAccess);
            var processed = processor.ProcessComplianceStep(query);
            return processed;
        }

        private void PrintReasons(ComplianceResult processed)
        {
            foreach (var result in processed.Results)
            foreach (var text in result.Messages)
                TestContext.WriteLine($"Test {result.TestName} run -> Passed?: {result.Passed} with {text}");
        }

        #endregion
    }
}