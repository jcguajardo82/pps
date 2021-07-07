using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.PaymentProcessor
{
    public sealed class TransactionReferenceResponse
    {
        [JsonProperty(JsonFieldNamesConstants.PAYMENT_PROCESSOR_TRANSACTION_REFERENCE_ID, Order = 1)]
        public long TransactionReferenceId { get; set; }
    }
}
