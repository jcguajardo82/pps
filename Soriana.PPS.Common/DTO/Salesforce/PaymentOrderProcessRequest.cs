using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.Enums;
using System.Collections.Generic;

namespace Soriana.PPS.Common.DTO.Salesforce
{
    public sealed class PaymentOrderProcessRequest
    {
        #region Constructors
        public PaymentOrderProcessRequest()
        {
            Shipments = new List<Shipment>();
            MerchantDefinedData = new List<MerchantDefinedData>();
        }
        #endregion
        #region Public Properties

        #region OrderInformation

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_ORDER_REFERENCE_NUMBER, Order = 1)]
        public string OrderReferenceNumber { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_ORDER_DATE_TIME, Order = 2)]
        public string OrderDateTime { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_ORDER_SALE_CHANNEL, Order = 3)]
        public string OrderSaleChannel { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_ORDER_COUPON_CODE, Order = 4)]
        public string OrderCouponCode { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_ORDER_AMOUNT, Order = 5)]
        public string OrderAmount { get; set; }
        #endregion

        #region PaymentInformation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_TYPE, Order = 6)]
        public string PaymentType { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_PROCESSOR, Order = 7)]
        public string PaymentProcessor { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_TOKEN, Order = 8)]
        public string PaymentToken { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_CARD_CVV, Order = 9)]
        public string PaymentCardCVV { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_CARD_NIP, Order = 10)]
        public string PaymentCardNIP { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_SAVE_CARD, Order = 11)]
        public string PaymentSaveCard { get; set; }
        #endregion

        #region CustomerInformation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_EMAIL, Order = 12)]
        public string CustomerEmail { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_FIRST_NAME, Order = 13)]
        public string CustomerFirstName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_LAST_NAME, Order = 14)]
        public string CustomerLastName { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_ADDRESS, Order = 15)]
        public string CustomerAddress { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_CITY, Order = 16)]
        public string CustomerCity { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_STATE, Order = 17)]
        public string CustomerState { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_ZIP_CODE, Order = 18)]
        public string CustomerZipCode { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_COUNTRY, Order = 19)]
        public string CustomerCountry { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_ID, Order = 20)]
        public string CustomerId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_DEVICE_FINGER_PRINT_ID, Order = 21)]
        public string CustomerDeviceFingerPrintId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_IP_ADDRESS, Order = 22)]
        public string CustomerIPAddress { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_PURCHASES_QUANTITY, Order = 23)]
        public string CustomerPurchasesQuantity { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_CONTACT, Order = 24)]
        public string CustomerContact { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_LOYALTY_CARD_ID, Order = 25)]
        public string CustomerLoyaltyCardId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_LOYALTY_REDEEM_ELECTRONIC_MONEY, Order = 26)]
        public string CustomerLoyaltyRedeemElectronicMoney { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_LOYALTY_REDEEM_POINTS, Order = 27)]
        public string CustomerLoyaltyRedeemPoints { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_LOYALTY_REDEEM_MONEY, Order = 28)]
        public string CustomerLoyaltyRedeemMoney { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_CUSTOMER_REGISTERED_DAYS, Order = 29)]
        public string CustomerRegisteredDays { get; set; }
        #endregion

        #region ShipmentInformation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPMENTS, Order = 30)]
        public IList<Shipment> Shipments { get; set; }
        #endregion

        #region Custom Additional Infomation
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_TRANSACTION_AUTHORIZATION_ID, Order = 31)]
        public string TransactionAuthorizationId { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_TOKENIZED_CARD_NUMBER, Order = 32)]
        public string TokenizedCardNumber { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_SEQUENCE_PAYMENT_ORDER, Order = 33)]
        public byte SequencePaymentOrder { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_MERCHANDISE_TYPE, Order = 34)]
        public MerchandiseTypeEnum MerchandiseType { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_MERCHANT_DEFINED_DATA, Order = 35)]
        public IList<MerchantDefinedData> MerchantDefinedData { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_TRANSACTION_REFERENCE_ID, Order = 36)]
        public long TransactionReferenceID { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_INTERNAL_CARDINAL_TOKEN, Order = 37)]
        public string InternalCardinalToken { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_IS_RETRYING, Order = 38)]
        public bool IsRetrying { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_AFFILIATION_TYPE, Order = 39)]
        public AffiliationTypeEnum AffiliationType { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_IS_AUTHENTICATED, Order = 40)]
        public bool IsAuthenticated { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_IS_AUTHORIZED, Order = 41)]
        public bool IsAuthorized { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_APPLY_3DS, Order = 42)]
        public bool Apply3DS { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_NOTIFY_AUTHENTICATION_STATUS, Order = 43)]
        public string NotifyAuthenticationStatus { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.PAYMENT_PROCESSOR_PAYMENT_ORDER_ID, Order = 44)]
        public long PaymentOrderID { get; set; }
        #endregion
        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_RETURN_URL, Order = 45)]
        public string ReturnURL { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_PAYMENT_TRANSACTION_SERVICE, Order = 46)]
        public string PaymentTransactionService { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_TRANSACTION_STATUS, Order = 47)]
        public string TransactionStatus { get; set; }
        #endregion
        #region Public Methods
        public PaymentOrderProcessRequest Clone()
        {
            return (PaymentOrderProcessRequest)this.MemberwiseClone();
        }
        #endregion
    }
}
