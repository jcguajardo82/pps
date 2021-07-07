using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Salesforce
{
    public sealed class Item
    {
        #region Public Properties
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_ID, Order = 1)]
        public string ShippingItemId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_EAN, Order = 2)]
        public string ShippingItemEAN { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_NAME, Order = 3)]
        public string ShippingItemName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_CATEGORY, Order = 4)]
        public string ShippingItemCategory { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_PRICE, Order = 5)]
        public double ShippingItemPrice { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_QUANTITY, Order = 6)]
        public int ShippingItemQuantity { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_ITEM_TOTAL, Order = 7)]
        public double ShippintItemTotal { get; set; }
        #endregion
    }
}
