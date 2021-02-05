﻿using System.Collections.Generic;
using LoanConformance.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoanConformance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConformanceQuery
    {
        [JsonRequired] public decimal LoanAmount { get; set; }

        [JsonRequired] public decimal AnnualPercentageRate { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public StateEnum State { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanTypeEnum LoanType { get; set; }

        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanOccupancyTypeEnum OccupancyType { get; set; }

        [JsonRequired] public List<LoanFeeAllocation> FeeAllocations { get; set; }
    }
}