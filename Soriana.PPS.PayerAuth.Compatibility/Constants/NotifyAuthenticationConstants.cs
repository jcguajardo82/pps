namespace Soriana.PPS.DecisionManager.Common.Compatibility.Constants
{
    public static class NotifyAuthenticationConstants
    {
        #region Constants
        public const string CONFIGURATION_BUG_CYBERSOURCE_OPTIONS = "CONFIGURATION BUG:  Cybersource options must not be null";
        public const string CONFIGURATION_BUG_PRODUCTION_URL_OR_SERVER_URL = "CONFIGURATION BUG:  sendToProduction or serverURL must be specified!";
        public const string CONFIGURATION_BUG_CERTIFICATE_IS_EXPIRED = "CONFIGURATION BUG:  sendToProduction or serverURL must be specified!";
        public const string CONFIGURATION_OR_CODE_BUG_MERCHANT_CERTIFICATE_IS_CERTIFICATE = "CONFIGURATION OR CODE BUG: merchant certificate is missing, check the p12 file";
        #endregion
    }
}
