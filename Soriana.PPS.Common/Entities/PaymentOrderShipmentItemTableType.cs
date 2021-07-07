using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_ORDER_SHIPMENT_ITEM)]
    public sealed class PaymentOrderShipmentItemTableType : EntityBase
    {
        [SourceNames(ColumnNameConstants.ORDER_REFERENCE_NUMER)]
        public long OrderReferenceNumber { get; set; }

        [SourceNames(ColumnNameConstants.SHIPMENT_ID_SEQUENCE)]
        public byte ShipmentIDSequence { get; set; }

        [SourceNames(ColumnNameConstants.ITEM_ID_SEQUENCE)]
        public byte ItemIDSequence { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_ID)]
        public int ShippingItemId { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_EAN)]
        public int ShippingItemEAN { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_NAME)]
        public string ShippingItemName { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_CATEGORY)]
        public string ShippingItemCategory { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_PRICE)]
        public double ShippingItemPrice { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_QUANTITY)]
        public byte ShippingItemQuantity { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ITEM_TOTAL)]
        public double ShippintItemTotal { get; set; }
    }
}
