namespace Soriana.PPS.Common.Constants
{
    public static class DatabaseSchemaConstants
    {
        #region Constants TABLE NAMES
        public const string TABLE_NAME_CLIENT_HAS_TOKEN = "tbl_Client_Has_Token";
        public const string TABLE_NAME_PAYMENT_PROCESS_TRANSACTION = "tbl_Payment_Process_Transaction";
        public const string TABLE_NAME_ITEM = "tbl_Item";
        public const string TABLE_NAME_PAYMENT_ORDER = "tbl_Payment_Order";
        public const string TABLE_NAME_PAYMENT_ORDER_JSON_REQUEST = "tbl_Payment_Order_Json_Request";
        public const string TABLE_NAME_PAYMENT_ORDER_SHIPMENT = "tbl_Payment_Order_Shipment";
        public const string TABLE_NAME_PAYMENT_ORDER_SHIPMENT_ITEM = "tbl_Payment_Order_Shipment_Item";
        public const string TABLE_NAME_PAYMENT_TRANSACTION = "tbl_Payment_Transaction";
        public const string TABLE_NAME_PAYMENT_TRANSACTION_JSON_REQUEST = "tbl_Payment_Transaction_Json_Request";
        public const string TABLE_NAME_PAYMENT_TRANSACTION_SHIPMENT = "tbl_Payment_Transaction_Shipment";
        public const string TABLE_NAME_PAYMENT_TRANSACTION_SHIPMENT_ITEM = "tbl_Payment_Transaction_Shipment_Item";
        public const string TABLE_NAME_PAYMENT_TRANSACTION_STATUS = "tbl_Payment_Transaction_Status";
        #endregion
        #region Constants TABLE TYPE NAMES
        public const string TABLE_TYPE_NAME_PAYMENT_TRANSACTION = "PaymentTransactionTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_TRANSACTION_JSON_REQUEST = "PaymentTransactionJsonRequestTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_TRANSACTION_SHIPMENT = "PaymentTransactionShipmentTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_TRANSACTION_SHIPMENT_ITEM = "PaymentTransactionShipmentItemTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_TRANSACTION_STATUS = "PaymentTransactionStatusTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_ORDER = "PaymentOrderTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_ORDER_JSON_REQUEST = "PaymentOrderJsonRequestTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_ORDER_SHIPMENT = "PaymentOrderShipmentTableType";
        public const string TABLE_TYPE_NAME_PAYMENT_ORDER_SHIPMENT_ITEM = "PaymentOrderShipmentItemTableType";
        #endregion

        #region Constants SEQUENCE NAMES
        public const string SEQUENCE_NAME_PAYMENT_TRANSACTION_ID = "PaymentTransactionIDSEQ";
        public const string SEQUENCE_NAME_PAYMENT_ORDER_ID = "PaymentOrderIDSEQ";
        #endregion
        #region Constants PROCEDURE NAMES
        public const string PROCEDURE_NAME_GET_PAYMENT_ORDER_SEQUENCE = "GetPaymentOrderSequence";
        public const string PROCEDURE_NAME_GET_PAYMENT_TRANSACTION_SEQUENCE = "GetPaymentTransactionSequence";
        public const string PROCEDURE_NAME_GET_CLIENT_HAS_TOKEN_BY = "GetClientHasTokenBy";
        public const string PROCEDURE_NAME_INSERT_PAYMENT_ORDER_REQUEST = "InsertPaymentOrderRequest";
        public const string PROCEDURE_NAME_INSERT_PAYMENT_TRANSACTION = "InsertPaymentTransaction";
        public const string PROCEDURE_NAME_INSERT_PAYMENT_TRANSACTION_STATUS = "InsertPaymentTransactionStatus";
        public const string PROCEDURE_NAME_UPDATE_PAYMENT_TRANSACTION_JSON_REQUEST = "UpdatePaymentTransactionJsonRequest";

        public const string PROCEDURE_NAME_GET_REQUEST_PAYMENT = "up_PPS_Sel_PaymenTransactionJsonReq";
        public const string PROCEDURE_NAME_GET_ORDER = "up_PPS_Sel_GetOrder";
        public const string PROCEDURE_NAME_GET_TRANSACTION = "up_PPS_Sel_PaymenTransactionOrder";
        #endregion
        #region Constants ERROR MESSAGE
        public const string ERROR_MESSAGE_SEARCH_FILTER = "Search filter parameter must not be null.";
        public const string ERROR_MESSAGE_ENTITY_MUST_NOT_BE_NULL = "Entity({0}) must not be null.";
        #endregion
        #region Constants SQL STATEMENT
        public const string SQL_STATEMENT_NEXT_VALUE_SEQUENCE = "SELECT NEXT VALUE FOR {0}";
        public const string SQL_STATEMENT_EXECUTE_PROCEDURE = "EXECUTE {0}";
        #endregion
    }
}
