using LoanConformance.Models.Enums;
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

        [JsonRequired] public decimal FeeCharged { get; set; }
    }
}