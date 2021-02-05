using System.Collections.Generic;
using LoanCompliance.Models.Data;

namespace LoanCompliance.Data
{
    public interface IDataAccess
    {
        IEnumerable<StateAprModel> GetAprData();
        IEnumerable<StateFeeModel> GetFeeData();
        IEnumerable<GlobalRulesetModel> GetGlobalRulesetData();
    }
}