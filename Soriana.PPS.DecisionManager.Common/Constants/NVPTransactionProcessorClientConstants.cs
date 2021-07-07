namespace Soriana.PPS.DecisionManager.Common.Constants
{
    public static class NVPTransactionProcessorClientConstants
    {
        #region Constants
        public const string CONFIGURATION_BUG_CYBERSOURCE_OPTIONS = "CONFIGURATION BUG: Cybersource options must not be null";
        public const string CONFIGURATION_BUG_PRODUCTION_URL_OR_SERVER_URL = "CONFIGURATION BUG: sendToProduction or serverURL must be specified!";
        public const string CONFIGURATION_BUG_CERTIFICATE_IS_EXPIRED = "CONFIGURATION BUG: sendToProduction or serverURL must be specified!";
        public const string CONFIGURATION_OR_CODE_BUG_MERCHANT_CERTIFICATE_IS_MISSING = "CONFIGURATION OR CODE BUG: merchant certificate is missing, check the p12 file";
        public const string CONFIGURATION_OR_CODE_BUG_CYBS_CERTIFICATE_IS_MISSING = "CONFIGURATION OR CODE BUG: cybs certificate is missing, check the p12 file";
        #endregion
        #region Constants Notify Validation Request
        public const string NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_RUN = "caseManagementActionService_run";
        public const string NOTIFY_VALIDATION_REQUEST_MERCHANT_REFERENCE_CODE = "merchantReferenceCode";
        public const string NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_ACTION_CODE = "caseManagementActionService_actionCode";
        public const string NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_REQUEST_ID = "caseManagementActionService_requestID";
        public const string NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_COMMENTS = "caseManagementActionService_comments";
        #endregion
    }
}
