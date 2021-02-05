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
            Results = new List<TestResult>();
        }

        public ComplianceResult(string testName, bool passed, string message)
        {
            Results = new List<TestResult> {new(testName, passed, message)};
        }

        public ComplianceResult(string testName, bool passed, params string[] messages)
        {
            var results = messages
                .Select(message => new TestResult(testName, passed, message))
                .ToList();
            Results = results;
        }

        [JsonRequired] public bool Success => Results.All(x => x.Passed);

        [JsonProperty(Required = Required.AllowNull)]
        public List<TestResult> Results { get; set; }

        /// <summary>
        ///     Allows us to use LINQ to aggregate results
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ComplianceResult operator +(ComplianceResult a, ComplianceResult b)
        {
            return new()
            {
                Results = a.Results.Concat(b.Results).ToList()
            };
        }
    }
}