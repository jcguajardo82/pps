using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using System;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_NAME_PAYMENT_ORDER)]
    public sealed class PaymentOrder : EntityBase
    {
        #region Public Properties
        [Key()]
        public long PaymentOrderID { get; set; }
        public long OrderReferenceNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public string OrderSaleChannel { get; set; }
        public string OrderCouponCode { get; set; }
        public double OrderAmount { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerDeviceFingerPrintId { get; set; }
        public string CustomerIPAddress { get; set; }
        public int CustomerPurchasesQuantity { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerLoyaltyCardId { get; set; }
        public int CustomerRegisteredDays { get; set; }
        public string ReturnURL { get; set; }
        public string PaymentType { get; set; }
        public string PaymentProcessor { get; set; }
        public string PaymentToken { get; set; }
        public bool PaymentSaveCard { get; set; }
        public short PaymentCardCVV { get; set; }
        public short PaymentCardNIP { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public int CustomerZipCode { get; set; }
        public string CustomerCountry { get; set; }
        public double CustomerLoyaltyRedeemMoney { get; set; }
        public double CustomerLoyaltyRedeemElectronicMoney { get; set; }
        public double CustomerLoyaltyRedeemPoints { get; set; }
        #endregion
    }
}
