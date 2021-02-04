using System;
using System.Collections.Generic;
using System.Linq;
using LoanConformance.Models;
using LoanConformance.Models.Data;

namespace LoanConformance.Data.InMemory.Impl
{
    public class InMemoryDataAccess : IDataAccess
    {
        public IEnumerable<StateAprModel> GetAprData()
        {
            var aprData = new List<StateAprModel>
                {
                    new()
                    {
                        State = StateEnum.NewYork,
                        LoanType = LoanTypeEnum.Conventional,
                        OccupancyType = LoanOccupancyTypeEnum.PrimaryOccupancy,
                        AnnualRatePercentage = 0.06m
                    },
                    new()
                    {
                        State = StateEnum.NewYork,
                        LoanType = LoanTypeEnum.Conventional,
                        OccupancyType = LoanOccupancyTypeEnum.SecondaryOccupancy,
                        AnnualRatePercentage = 0.08m
                    }
                }
                .Concat(GetAprDataForAllLoanTypes(StateEnum.Virginia, LoanOccupancyTypeEnum.PrimaryOccupancy, 0.05m))
                .Concat(GetAprDataForAllLoanTypes(StateEnum.Virginia, LoanOccupancyTypeEnum.SecondaryOccupancy, 0.08m))
                .Concat(GetAprDataForAllLoanTypes(StateEnum.Maryland, 0.04m))
                .Concat(new List<StateAprModel>
                {
                    new()
                    {
                        State = StateEnum.California,
                        LoanType = LoanTypeEnum.Conventional,
                        OccupancyType = LoanOccupancyTypeEnum.PrimaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    },
                    new()
                    {
                        State = StateEnum.California,
                        LoanType = LoanTypeEnum.Conventional,
                        OccupancyType = LoanOccupancyTypeEnum.SecondaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    },
                    new()
                    {
                        State = StateEnum.California,
                        LoanType = LoanTypeEnum.FHA,
                        OccupancyType = LoanOccupancyTypeEnum.PrimaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    },
                    new()
                    {
                        State = StateEnum.California,
                        LoanType = LoanTypeEnum.FHA,
                        OccupancyType = LoanOccupancyTypeEnum.SecondaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    }
                })
                .Concat(GetAprDataForAllLoanTypes(StateEnum.California, LoanTypeEnum.VA, 0.03m));
            return aprData;
        }

        public IEnumerable<StateFeeModel> GetFeeData()
        {
            var result = new List<StateFeeModel>()
                .Concat(GetStateFeeDataForFeeTypes(StateEnum.Virginia, LoanFeeTypeEnum.FloodCertification, LoanFeeTypeEnum.Processing, LoanFeeTypeEnum.Settlement))
                .Concat(GetStateFeeDataForFeeTypes(StateEnum.Maryland, LoanFeeTypeEnum.Application, LoanFeeTypeEnum.CreditReport))
                .Concat(GetStateFeeDataForFeeTypes(StateEnum.California, LoanFeeTypeEnum.Application, LoanFeeTypeEnum.Settlement))
                .Concat(GetStateFeeDataForFeeTypes(StateEnum.Florida, LoanFeeTypeEnum.Application, LoanFeeTypeEnum.FloodCertification, LoanFeeTypeEnum.TitleSearch));
            return result;
        }

        public IEnumerable<GlobalRulesetModel> GetGlobalRuleset()
        {
            var result = new List<GlobalRulesetModel>()
                .Concat(GetGlobalRulesetForLoanTypes(StateEnum.NewYork, 750_000.00m, LoanTypeEnum.Conventional))
                .Concat(GetGlobalRulesetForLoanTypes(StateEnum.Virginia, decimal.MaxValue, LoanTypeEnum.VA,
                    LoanTypeEnum.Conventional, LoanTypeEnum.FHA))
                .Concat(GetGlobalRulesetForLoanTypes(StateEnum.Maryland, 400_000.00m));
            return result;
        }

        public static IEnumerable<StateFeeRangeModel> GetStateFeeRanges()
        {
            var result = new List<StateFeeRangeModel>
            {
                new() { State = StateEnum.Virginia, LowerValue = 0, UpperValue = decimal.MaxValue, PercentageCharged = 0.07m },
                new() { State = StateEnum.Maryland, LowerValue = 0, UpperValue = 200_000.00m, PercentageCharged = 0.04m },
                new() { State = StateEnum.Maryland, LowerValue = 200_000.00m, UpperValue = decimal.MaxValue, PercentageCharged = 0.06m },
                new() { State = StateEnum.California, LowerValue = 0, UpperValue = 50_000.00m, PercentageCharged = 0.03m },
                new() { State = StateEnum.California, LowerValue = 50_000.00m, UpperValue = 150_000.00m, PercentageCharged = 0.04m },
                new() { State = StateEnum.California, LowerValue = 150_000.00m, UpperValue = decimal.MaxValue, PercentageCharged = 0.05m },
                new() { State = StateEnum.Florida, LowerValue = 0, UpperValue = 20_000.00m, PercentageCharged = 0.06m },
                new() { State = StateEnum.Florida, LowerValue = 20_000.00m, UpperValue = 75_000.00m, PercentageCharged = 0.08m },
                new() { State = StateEnum.Florida, LowerValue = 75_000.00m, UpperValue = 150_000.00m, PercentageCharged = 0.09m },
                new() { State = StateEnum.Florida, LowerValue = 150_000.00m, UpperValue = decimal.MaxValue, PercentageCharged = 0.1m },
            };
            return result;
        }

        #region Data Generators

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(StateEnum state, decimal percentageAmount)
        {
            var result = from loanType in (LoanTypeEnum[]) Enum.GetValues(typeof(LoanTypeEnum))
                from occupancy in (LoanOccupancyTypeEnum[]) Enum.GetValues(typeof(LoanOccupancyTypeEnum))
                select new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                };
            return result;
        }

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(StateEnum state,
            LoanOccupancyTypeEnum occupancy, decimal percentageAmount)
        {
            var result = ((LoanTypeEnum[]) Enum.GetValues(typeof(LoanTypeEnum)))
                .Select(loanType => new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                });
            return result;
        }

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(StateEnum state, LoanTypeEnum loanType,
            decimal percentageAmount)
        {
            var result = ((LoanOccupancyTypeEnum[]) Enum.GetValues(typeof(LoanOccupancyTypeEnum)))
                .Select(occupancy => new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                });
            return result;
        }

        private static IEnumerable<GlobalRulesetModel> GetGlobalRulesetForLoanTypes(StateEnum state,
            decimal maximumLoanAmount,
            params LoanTypeEnum[] loanTypes)
        {
            var result = loanTypes
                .Select(loanType => new GlobalRulesetModel
                {
                    ApplicableLoanType = loanType,
                    MaximumLoanAmount = maximumLoanAmount,
                    State = state
                });
            return result;
        }

        private static IEnumerable<StateFeeModel> GetStateFeeDataForFeeTypes(StateEnum state, params LoanFeeTypeEnum[] loanFeeTypes)
        {
            var stateFeeRanges = GetStateFeeRanges().ToList();
            var result = loanFeeTypes
                .Select(loanFeeType => new StateFeeModel
                {
                    State = state,
                    LoanFeeTypes = loanFeeType,
                    MaxChargeFeeRanges = stateFeeRanges.Where(x => x.State == state)
                });
            return result;
        }

        #endregion
    }
}