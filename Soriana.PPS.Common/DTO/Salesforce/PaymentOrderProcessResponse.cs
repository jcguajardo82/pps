using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using System.Collections.Generic;

namespace Soriana.PPS.Common.DTO.Salesforce
{
    public sealed class PaymentOrderProcessResponse
    {
        #region Constructors
        public PaymentOrderProcessResponse()
        {
            Shipments = new List<ShipmentResponse>();
        }
        #endregion
        #region Public Properties
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_ERROR, Order = 1)]
        public string ResponseError { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_ERROR_TEXT, Order = 2)]
        public string ResponseErrorText { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_CODE, Order = 3)]
        public string ResponseCode { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RESPONSE_MESSAGE, Order = 4)]
        public string ResponseMessage { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_RECEIPT_BARCODE, Order = 5)]
        public string ReceiptBarcode { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RESPONSE_SHIPMENTS, Order = 6)]
        public IList<ShipmentResponse> Shipments { get; set; }
        #endregion
    }
}
