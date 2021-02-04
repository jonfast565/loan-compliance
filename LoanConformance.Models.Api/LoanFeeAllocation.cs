using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanConformance.Models.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoanConformance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LoanFeeAllocation
    {
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanFeeTypeEnum LoanFeeType { get; set; }

        [JsonRequired]
        public decimal FeeCharged { get; set; }
    }
}
