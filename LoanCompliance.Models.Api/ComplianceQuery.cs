using System.Collections.Generic;
using LoanConformance.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoanConformance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ComplianceQuery
    {
        [JsonRequired] public decimal LoanAmount { get; set; }

        [JsonRequired] public decimal AnnualPercentageRate { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public State State { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanType LoanType { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanOccupancyType OccupancyType { get; set; }

        [JsonRequired] public List<LoanFeeAllocation> FeeAllocations { get; set; }
    }
}