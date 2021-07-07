using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;
using System;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_ORDER)]
    public sealed class PaymentOrderTableType : EntityBase
    {
        [SourceNames(ColumnNameConstants.ORDER_REFERENCE_NUMER)]
        public long OrderReferenceNumber { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_DATE)]
        public DateTime OrderDate { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_TIME)]
        public TimeSpan OrderTime { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_SALE_CHANNEL)]
        public string OrderSaleChannel { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_COUPON_CODE)]
        public string OrderCouponCode { get; set; }

        [SourceNames(ColumnNameConstants.ORDER_AMOUNT)]
        public double OrderAmount { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_EMAIL)]
        public string CustomerEmail { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_FIRST_NAME)]
        public string CustomerFirstName { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_LAST_NAME)]
        public string CustomerLastName { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_ID)]
        public string CustomerId { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_DEVICE_FINGER_PRINT_ID)]
        public string CustomerDeviceFingerPrintId { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_IP_ADDRESS)]
        public string CustomerIPAddress { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_PURCHASES_QUANTITY)]
        public int CustomerPurchasesQuantity { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_CONTACT)]
        public string CustomerContact { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_LOTALTY_CARD_ID)]
        public string CustomerLoyaltyCardId { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_REGISTERED_DAYS)]
        public int CustomerRegisteredDays { get; set; }

        [SourceNames(ColumnNameConstants.RETURN_URL)]
        public string ReturnURL { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_TYPE)]
        public string PaymentType { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_PROCESSOR)]
        public string PaymentProcessor { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_TOKEN)]
        public string PaymentToken { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_SAVE_CARD)]
        public bool PaymentSaveCard { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_CARD_CVV)]
        public short PaymentCardCVV { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_CARD_NIP)]
        public short PaymentCardNIP { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_ADDRESS)]
        public string CustomerAddress { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_CITY)]
        public string CustomerCity { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_STATE)]
        public string CustomerState { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_ZIP_CODE)]
        public int CustomerZipCode { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_COUNTRY)]
        public string CustomerCountry { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_MONEY)]
        public double CustomerLoyaltyRedeemMoney { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_ELECTRONIC_MONEY)]
        public double CustomerLoyaltyRedeemElectronicMoney { get; set; }

        [SourceNames(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_POINTS)]
        public double CustomerLoyaltyRedeemPoints { get; set; }
    }
}
