using LoanConformance.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoanConformance.Models.Data;

namespace LoanConformance.Data
{
    public interface IDataAccess
    {
        IEnumerable<StateAprModel> GetAprData();
        IEnumerable<StateFeeModel> GetFeeData();
        IEnumerable<GlobalRulesetModel> GetGlobalRuleset();
    }
}
