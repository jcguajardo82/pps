namespace Soriana.PPS.Common.Constants
{
    public static class PaymentProcessorConstants
    {
        #region Contants Merchant Defined Data
        public const string MERCHANT_DEFINED_DATA_PAYMENT_METHOD_FIELD = "PaymentMethodTBD";
        public const string MERCHANT_DEFINED_DATA_BIN_FIELD = "BinTBD";
        public const string MERCHANT_DEFINED_DATA_BANK_FIELD = "BankTBD";
        public const string MERCHANT_DEFINED_DATA_TYPE_OF_CARD_FIELD = "TypeOfCardTBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_1_FIELD = "Consigment1TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_2_FIELD = "Consigment2TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_3_FIELD = "Consigment3TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_4_FIELD = "Consigment4TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_5_FIELD = "Consigment5TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_6_FIELD = "Consigment6TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_1_FIELD = "ConsigmentAmount1TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_2_FIELD = "ConsigmentAmount2TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_3_FIELD = "ConsigmentAmount3TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_4_FIELD = "ConsigmentAmount4TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_5_FIELD = "ConsigmentAmount5TBD";
        public const string MERCHANT_DEFINED_DATA_CONSIGMENT_AMOUNT_6_FIELD = "ConsigmentAmount6TBD";
        public const string MERCHANT_DEFINED_DATA_NO_APPLY_FIELD = "N/A";
        #endregion
        #region Contants Messages
        public const string PAYMENT_FAILED_OTHER_PAYMENT_FORM = "Favor de intentar con otra forma de pago.";
        public const string PAYMENT_FAILED_INTERNAL_ERROR = "Un error ha ocurrido, reintente de nuevo o favor de contactar al administrador.";
        public const string PAYMENT_FAILED_PENDING_AUTHENTICATION = "Transacción pendiente por autenticación, favor de enviar datos de inicialización requeridos.";
        public const string PAYMENT_FAILED_DECISION_PENDING_AUTHENTICATION = "Transacción pendiente por autenticación, favor de iniciar enrolamiento.";
        public const string PAYMENT_FAILED_DECISION_PENDING_REVIEW = "Transacción pendiente por revisión manual.";
        public const string PAYMENT_FAILED_PROCESS_PAYMENT_PENDING_REVIEW = "Transacción pendiente por revisión manual.";
        public const string PAYMENT_FAILED_PROCESS_PAYMENT_PENDING_AUTHENTICATION = "Transacción pendiente por autenticación, favor de iniciar enrolamiento.";
        public const string PAYMENT_FAILED_PROCESS_PAYMENT_DECLINED = "Transacción declinada.";
        public const string PAYMENT_FAILED_PROCESS_PAYMENT_INVALID_REQUEST = "Solicitud de transacción invalida.";
        #endregion

        #region Contants Payment Order Process
        public const string PAYMENT_ORDER_PROCESS_VALIDATION_3DS = "3DS";
        public const string PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REASON_CODE = "caseManagementActionReply_reasonCode";
        public const string PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_MERCHANT_REFERENCE_CODE = "merchantReferenceCode";
        public const string PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REQUEST_ID = "requestID";
        public const string PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_ACCEPT = "ACCEPT";
        public const string PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REJECT = "REJECT";
        public const string PAYMENT_ORDER_PROCESS_PAYMENT_TYPE_ID = "1";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_VISA = "001";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_MASTERCARD = "002";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_AMERICAN_EXPRESS = "003";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_DEFAULT = "000";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_VISA = "VISA";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_MASTERCARD = "MASTERCARD";
        public const string PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_AMERICAN_EXPRESS = "AMERICANEXPRESS";
        #endregion
    }
}
