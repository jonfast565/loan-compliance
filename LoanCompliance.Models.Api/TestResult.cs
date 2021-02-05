using System.Collections.Generic;
using System.Linq;

namespace LoanCompliance.Models.Api
{
    public class TestResult
    {
        public TestResult(string testName, bool passed, string message)
        {
            TestName = testName;
            Passed = passed;
            Messages = new List<string> {message};
        }

        public TestResult(string testName, bool passed, params string[] messages)
        {
            TestName = testName;
            Passed = passed;
            Messages = messages.ToList();
        }

        public List<string> Messages { get; set; }
        public string TestName { get; set; }
        public bool Passed { get; set; }
    }
}