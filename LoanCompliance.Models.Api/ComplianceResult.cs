using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LoanConformance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ComplianceResult
    {
        public ComplianceResult()
        {
            Success = true;
            Reasons = new List<string>();
        }

        public ComplianceResult(List<string> failures)
        {
            Success = false;
            Reasons = failures;
        }

        public ComplianceResult(string failure)
        {
            Success = false;
            Reasons = new List<string> {failure};
        }

        [JsonRequired] public bool Success { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public List<string> Reasons { get; set; }

        public static ComplianceResult operator +(ComplianceResult a, ComplianceResult b)
        {
            return new()
            {
                Success = a.Success && b.Success,
                Reasons = a.Reasons.Concat(b.Reasons).ToList()
            };
        }
    }
}