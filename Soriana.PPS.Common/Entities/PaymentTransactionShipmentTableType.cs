using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_TRANSACTION_SHIPMENT)]
    public sealed class PaymentTransactionShipmentTableType : EntityBase
    {
        [SourceNames(ColumnNameConstants.PAYMENT_TRANSACTION_ID)]
        public long PaymentTransactionID { get; set; }

        [SourceNames(ColumnNameConstants.SHIPMENT_ID_SEQUENCE)]
        public byte ShipmentIDSequence { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_ORDER_ID)]
        public long PaymentOrderID { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_REFERENCE_NUMER)]
        public long OrderReferenceNumber { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_REFERENCE_NUMBER)]
        public string ShippingReferenceNumber { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_STORE_ID)]
        public string ShippingStoreId { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_STORE_NAME)]
        public string ShippingStoreName { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_DELIVERY_ID)]
        public string ShippingDeliveryId { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_DELIVERY_DESC)]
        public string ShippingDeliveryDesc { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_PAYMENT_INSTALLMENTS)]
        public int ShippingPaymentInstallments { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_PAYMENT_IMPORT)]
        public double ShippingPaymentImport { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_FIRST_NAME)]
        public string ShippingFirstName { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_LAST_NAME)]
        public string ShippingLastName { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_ADDRESS)]
        public string ShippingAddress { get; set; }

        [SourceNames(ColumnNameConstants.SHIPPING_CITY)]
        public string ShippingCity { get; set; }
    }
}
