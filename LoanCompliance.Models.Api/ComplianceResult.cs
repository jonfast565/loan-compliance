using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LoanCompliance.Models.Api
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

        public ComplianceResult(bool success, string reason)
        {
            Success = success;
            Reasons = new List<string> {reason};
        }

        [JsonIgnore]
        public bool Skip { get; set; }

        [JsonRequired] public bool Success { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public List<string> Reasons { get; set; }

        /// <summary>
        /// Allows us to use LINQ to aggregate results
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplianceResult operator +(ComplianceResult a, ComplianceResult b)
        {
            return new()
            {
                Success = a.Success && b.Success,
                Reasons = a.Reasons.Concat(b.Reasons).ToList(),
                Skip = a.Skip || b.Skip
            };
        }
    }
}