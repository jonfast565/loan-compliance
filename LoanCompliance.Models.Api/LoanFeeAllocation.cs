using LoanCompliance.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoanCompliance.Models.Api
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LoanFeeAllocation
    {
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanFeeType LoanFeeType { get; set; }

        [JsonRequired] public decimal FeeCharged { get; set; }
    }
}