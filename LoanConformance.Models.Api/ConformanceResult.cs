using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LoanConformance.Models.Query
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConformanceResult
    {
        [JsonRequired]
        public bool ComplianceCheckNeeded { get; set; }
        
        [JsonRequired]
        public bool Complies { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public List<string> FailureReasons { get; set; }

        public static ConformanceResult operator +(ConformanceResult a, ConformanceResult b)
        {
            return new()
            {
                ComplianceCheckNeeded = a.ComplianceCheckNeeded && b.ComplianceCheckNeeded,
                Complies = a.Complies && b.Complies,
                FailureReasons = a.FailureReasons.Concat(b.FailureReasons).ToList()
            };
        }
    }
}
