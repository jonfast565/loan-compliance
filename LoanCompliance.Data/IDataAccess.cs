using System.Collections.Generic;
using LoanConformance.Models.Data;

namespace LoanConformance.Data
{
    public interface IDataAccess
    {
        IEnumerable<StateAprModel> GetAprData();
        IEnumerable<StateFeeModel> GetFeeData();
        IEnumerable<GlobalRulesetModel> GetGlobalRulesetData();
    }
}