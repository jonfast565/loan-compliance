using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LoanConformance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConformanceResult
    {
        public ConformanceResult()
        {
            Success = true;
            Reasons = new List<string>();
        }

        public ConformanceResult(List<string> failures)
        {
            Success = false;
            Reasons = failures;
        }

        public ConformanceResult(string failure)
        {
            Success = false;
            Reasons = new List<string> {failure};
        }

        [JsonRequired] public bool Success { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public List<string> Reasons { get; set; }

        public static ConformanceResult operator +(ConformanceResult a, ConformanceResult b)
        {
            return new()
            {
                Success = a.Success && b.Success,
                Reasons = a.Reasons.Concat(b.Reasons).ToList()
            };
        }
    }
}