using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Salesforce
{
    public sealed class ShipmentResponse
    {
        #region Public Properties
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_SHIPPING_REFERENCE_NUMBER, Order = 1)]
        public string ShippingReferenceNumber { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_REQUEST_ID, Order = 2)]
        public string RequestId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_CODE, Order = 3)]
        public string ResponseCode { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_MESSAGE, Order = 4)]
        public string ResponseMessage { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_AUTH_CODE, Order = 5)]
        public string ResponseAuthCode { get; set; }
        #endregion
    }
}
