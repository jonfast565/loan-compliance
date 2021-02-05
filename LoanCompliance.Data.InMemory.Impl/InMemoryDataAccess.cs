using System;
using System.Collections.Generic;
using System.Linq;
using LoanCompliance.Models.Data;
using LoanCompliance.Models.Enums;

namespace LoanCompliance.Data.InMemory.Impl
{
    public class InMemoryDataAccess : IDataAccess
    {
        public IEnumerable<StateAprModel> GetAprData()
        {
            var aprData = new List<StateAprModel>
                {
                    new()
                    {
                        State = State.NewYork,
                        LoanType = LoanType.Conventional,
                        OccupancyType = LoanOccupancyType.PrimaryOccupancy,
                        AnnualRatePercentage = 0.06m
                    },
                    new()
                    {
                        State = State.NewYork,
                        LoanType = LoanType.Conventional,
                        OccupancyType = LoanOccupancyType.SecondaryOccupancy,
                        AnnualRatePercentage = 0.08m
                    }
                }
                .Concat(GetAprDataForAllLoanTypes(State.Virginia, LoanOccupancyType.PrimaryOccupancy, 0.05m))
                .Concat(GetAprDataForAllLoanTypes(State.Virginia, LoanOccupancyType.SecondaryOccupancy, 0.08m))
                .Concat(GetAprDataForAllLoanTypes(State.Maryland, 0.04m))
                .Concat(new List<StateAprModel>
                {
                    new()
                    {
                        State = State.California,
                        LoanType = LoanType.Conventional,
                        OccupancyType = LoanOccupancyType.PrimaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    },
                    new()
                    {
                        State = State.California,
                        LoanType = LoanType.Conventional,
                        OccupancyType = LoanOccupancyType.SecondaryOccupancy,
                        AnnualRatePercentage = 0.04m
                    },
                    new()
                    {
                        State = State.California,
                        LoanType = LoanType.FHA,
                        OccupancyType = LoanOccupancyType.PrimaryOccupancy,
                        AnnualRatePercentage = 0.05m
                    },
                    new()
                    {
                        State = State.California,
                        LoanType = LoanType.FHA,
                        OccupancyType = LoanOccupancyType.SecondaryOccupancy,
                        AnnualRatePercentage = 0.04m
                    }
                })
                .Concat(GetAprDataForAllLoanTypes(State.California, LoanType.VA, 0.03m));
            return aprData;
        }

        public IEnumerable<StateApplicableFeeModel> GetApplicableFeeData()
        {
            var result = new List<StateApplicableFeeModel>()
                .Concat(GetStateFeeDataForFeeTypes(State.Virginia, LoanFeeType.FloodCertification,
                    LoanFeeType.Processing, LoanFeeType.Settlement))
                .Concat(GetStateFeeDataForFeeTypes(State.Maryland, LoanFeeType.Application,
                    LoanFeeType.CreditReport))
                .Concat(GetStateFeeDataForFeeTypes(State.California, LoanFeeType.Application,
                    LoanFeeType.Settlement))
                .Concat(GetStateFeeDataForFeeTypes(State.Florida, LoanFeeType.Application,
                    LoanFeeType.FloodCertification, LoanFeeType.TitleSearch));
            return result;
        }

        public IEnumerable<GlobalRulesetModel> GetGlobalRulesetData()
        {
            var result = new List<GlobalRulesetModel>()
                .Concat(GetGlobalRulesetForLoanTypes(State.NewYork, 750_000.00m, LoanType.Conventional))
                .Concat(GetGlobalRulesetForLoanTypes(State.Virginia, decimal.MaxValue, LoanType.VA,
                    LoanType.Conventional, LoanType.FHA))
                .Concat(GetGlobalRulesetForLoanTypes(State.Maryland, 400_000.00m, LoanType.VA, LoanType.FHA, LoanType.Conventional))
                .Concat(GetGlobalRulesetForLoanTypes(State.Florida, decimal.MaxValue, LoanType.Conventional,
                    LoanType.VA))
                .Concat(GetGlobalRulesetForLoanTypes(State.California, 600_000.00m, LoanType.Conventional, LoanType.FHA, LoanType.VA));
            return result;
        }

        public IEnumerable<StateFeeRangeModel> GetFeeRangeData()
        {
            var result = new List<StateFeeRangeModel>
            {
                new()
                {
                    State = State.Virginia, LowerValue = 0, UpperValue = decimal.MaxValue, PercentageCharged = 0.07m
                },
                new() {State = State.Maryland, LowerValue = 0, UpperValue = 200_000.00m, PercentageCharged = 0.04m},
                new()
                {
                    State = State.Maryland, LowerValue = 200_000.00m, UpperValue = decimal.MaxValue,
                    PercentageCharged = 0.06m
                },
                new()
                {
                    State = State.California, LowerValue = 0, UpperValue = 50_000.00m, PercentageCharged = 0.03m
                },
                new()
                {
                    State = State.California, LowerValue = 50_000.00m, UpperValue = 150_000.00m,
                    PercentageCharged = 0.04m
                },
                new()
                {
                    State = State.California, LowerValue = 150_000.00m, UpperValue = decimal.MaxValue,
                    PercentageCharged = 0.05m
                },
                new() {State = State.Florida, LowerValue = 0, UpperValue = 20_000.00m, PercentageCharged = 0.06m},
                new()
                {
                    State = State.Florida, LowerValue = 20_000.00m, UpperValue = 75_000.00m,
                    PercentageCharged = 0.08m
                },
                new()
                {
                    State = State.Florida, LowerValue = 75_000.00m, UpperValue = 150_000.00m,
                    PercentageCharged = 0.09m
                },
                new()
                {
                    State = State.Florida, LowerValue = 150_000.00m, UpperValue = decimal.MaxValue,
                    PercentageCharged = 0.1m
                }
            };
            return result;
        }

        #region Data Generators

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(State state, decimal percentageAmount)
        {
            var result = from loanType in (LoanType[]) Enum.GetValues(typeof(LoanType))
                from occupancy in (LoanOccupancyType[]) Enum.GetValues(typeof(LoanOccupancyType))
                select new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                };
            return result;
        }

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(State state,
            LoanOccupancyType occupancy, decimal percentageAmount)
        {
            var result = ((LoanType[]) Enum.GetValues(typeof(LoanType)))
                .Select(loanType => new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                });
            return result;
        }

        private static IEnumerable<StateAprModel> GetAprDataForAllLoanTypes(State state, LoanType loanType,
            decimal percentageAmount)
        {
            var result = ((LoanOccupancyType[]) Enum.GetValues(typeof(LoanOccupancyType)))
                .Select(occupancy => new StateAprModel
                {
                    AnnualRatePercentage = percentageAmount,
                    LoanType = loanType,
                    OccupancyType = occupancy,
                    State = state
                });
            return result;
        }

        private static IEnumerable<GlobalRulesetModel> GetGlobalRulesetForLoanTypes(State state,
            decimal maximumLoanAmount,
            params LoanType[] loanTypes)
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

        private static IEnumerable<StateApplicableFeeModel> GetStateFeeDataForFeeTypes(State state,
            params LoanFeeType[] loanFeeTypes)
        {
            var result = loanFeeTypes
                .Select(loanFeeType => new StateApplicableFeeModel
                {
                    State = state,
                    LoanFeeType = loanFeeType
                });
            return result;
        }

        #endregion
    }
}