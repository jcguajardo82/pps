using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class PaymentProcessJWTResponse
    {
        [JsonProperty(JsonFieldNamesConstants.PAYMENT_PROCESSOR_JWT, Order = 1)]
        public string PaymentProcessJWT { get; set; }
    }
}
