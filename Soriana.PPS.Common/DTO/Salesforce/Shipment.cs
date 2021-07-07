using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using System.Collections.Generic;

namespace Soriana.PPS.Common.DTO.Salesforce
{
    public sealed class Shipment
    {
        #region Constructors
        public Shipment()
        {
            Items = new List<Item>();
        }
        #endregion
        #region Public Properties

        #region ShippingInformation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_REFERENCE_NUMBER, Order = 1)]
        public string ShippingReferenceNumber { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_STORE_ID, Order = 2)]
        public string ShippingStoreId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_STORE_NAME, Order = 3)]
        public string ShippingStoreName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_DELIVERY_ID, Order = 4)]
        public string ShippingDeliveryId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_DELIVERY_DESC, Order = 5)]
        public string ShippingDeliveryDesc { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_PAYMENT_INSTALLMENTS, Order = 6)]
        public string ShippingPaymentInstallments { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_PAYMENT_IMPORT, Order = 7)]
        public string ShippingPaymentImport { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_FIRST_NAME, Order = 8)]
        public string ShippingFirstName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_LAST_NAME, Order = 9)]
        public string ShippingLastName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ADDRESS, Order = 10)]
        public string ShippingAddress { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_CITY, Order = 11)]
        public string ShippingCity { get; set; }
        #endregion

        #region ItemInformation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEMS, Order = 12)]
        public IList<Item> Items { get; set; }
        #endregion

        #endregion
    }
}