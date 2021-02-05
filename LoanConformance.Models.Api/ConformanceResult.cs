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
            FailureReasons = new List<string>();
        }

        public ConformanceResult(List<string> failures)
        {
            Success = false;
            FailureReasons = failures;
        }

        public ConformanceResult(string failure)
        {
            Success = false;
            FailureReasons = new List<string> {failure};
        }

        [JsonRequired] public bool Success { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public List<string> FailureReasons { get; set; }

        public static ConformanceResult operator +(ConformanceResult a, ConformanceResult b)
        {
            return new()
            {
                Success = a.Success && b.Success,
                FailureReasons = a.FailureReasons.Concat(b.FailureReasons).ToList()
            };
        }
    }
}