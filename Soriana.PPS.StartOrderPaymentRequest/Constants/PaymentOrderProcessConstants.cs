namespace Soriana.PPS.PaymentOrderProcess.Constants
{
    public static class PaymentOrderProcessConstants
    {
        #region Constants
        public const string PAYMENT_ORDER_PROCESS_FUNCTION_NAME = "PaymentOrderProcess";
        public const string PAYMENT_ORDER_PROCESS_SERVICE_NAME = "PaymentOrderProcessService";
        public const string PAYMENT_ORDER_PROCESS_SERVICE_METHOD_NAME = "PaymentOrderProcess";
        public const string PAYMENT_ORDER_PROCESS_NO_CONTENT_REQUEST = "No Content Request";
        public const string PAYMENT_ORDER_PROCESS_INVALID_ORDER_AMOUNT = "Invalid order amount.";
        public const string SEQUENCE_PAYMENT_ORDER_TRANSACTION_SERVICE_NAME = "SequencePaymentOrderTransactionService";
        public const string SPLIT_PAYMENT_ORDER_SERVICE_NAME = "SplitPaymentOrderService";
        public const string MERCHANT_DEFINED_DATA_SERVICE_NAME = "MerchantDefinedDataService";
        public const string SAVE_PAYMENT_ORDER_SERVICE_NAME = "SavePaymentOrderService";
        public const string SAVE_PAYMENT_TRANSACTION_SERVICE_NAME = "SavePaymentTransactionService";
        public const string SAVE_PAYMENT_TRANSACTION_STATUS_SERVICE_NAME = "SavePaymentTransactionStatusService";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_BIN_NUMBER = "Invalid bin number {0}, can not be possible calculating PaymentMethod, Bank and TypeOfCard.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_BIN = "Invalid merchant defined data bin code.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_BANK = "Invalid merchant defined data bank.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_PAYMENT_METHOD = "Invalid merchant defined data payment method.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_TYPE_OF_CARD = "Invalid merchant defined data type of card.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_CLIENT_HAS_TOKEN = "Invalid client or token. Can not be possible calculating BinCode, PaymentMethod, Bank and TypeOfCard.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_GROCERY_CONFIGURATION_NUMBER = "Invalid Grocery Configuration Number.";
        public const string MERCHANT_DEFINED_DATA_SERVICE_INVALID_NON_GROCERY_CONFIGURATION_NUMBER = "Invalid Non Grocery Configuration Number.";
        public const string SPLIT_PAYMENT_ORDER_SERVICE_METHOD_NAME = "SplitPaymentOrderByMerchandiseType";
        public const string SPLIT_PAYMENT_ORDER_SERVICE_NO_VALIDATE_RESPONSE = "{0} service does not apply validate response";
        public const string MERCHANT_DEFINED_DATA_SERVICE_NO_VALIDATE_RESPONSE = "{0} service does not apply validate response";
        public const string SEQUENCE_PAYMENT_ORDER_TRANSACTION_SERVICE_NO_VALIDATE_RESPONSE = "{0} service does not apply validate response";
        public const string SAVE_PAYMENT_TRANSACTION_STATUS_SERVICE_NO_VALIDATE_RESPONSE = "{0} service does not apply validate response";
        public const string SAVE_PAYMENT_TRANSACTION_SERVICE_NO_VALIDATE_RESPONSE = "{0} service does not apply validate response";
        public const string CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_OPTIONS = "CONFIGURATION BUG:  Merchant defined data options must not be null";
        public const string CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_GROCERY_CONFIGURATION_NUMBER = "CONFIGURATION BUG: Grocery confiuration number is incorrect.";
        public const string CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_NON_GROCERY_CONFIGURATION_NUMBER = "CONFIGURATION BUG: Grocery confiuration number is incorrect.";
        public const string CONFIGURATION_BUG_MERCHANDISE_TYPE = "Does not exists other kind of merchandise distinct to grocery and nongrocery.";
        public const string CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES = "RetryByTechnicalIssues";
        public const string PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO = "Retrying issue over calling to service {0}.";
        public const string PAYMENT_ORDER_PROCESS_UNAVAILABLE_SERVICE = "Service {0} is unavailable.";
        public const string PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE = "Unsupported service response from calling {0} service.";
        public const string PAYMENT_ORDER_PROCESS_CYBERSOURCE_EXCEPTION = "Cybersource integration exception over calling {0} service.";
        public const string PAYMENT_ORDER_PROCESS_BUSINESS_EXCEPTION = "Business integration exception over calling {0} service.";
        public const string PAYMENT_ORDER_PROCESS_REQUEST_MESSAGE_SUCCESSFUL = "The '{0}' transaction was executed successfuly.";
        #endregion
    }
}
