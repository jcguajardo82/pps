namespace Soriana.PPS.DecisionManager.Common.Compatibility.Configuration
{
    public sealed class CybersourceOptions
    {
        #region Public Constants
        public const string CYBERSOURCE_OPTIONS_CONFIGURATION = "CybersourceOptions";
        public const string CYBERSOURCE_P12_EXTENSION = ".p12";
        #endregion
        #region Public Properties
        public string AuthenticationType { get; set; }

        public string MerchantID { get; set; }

        public string MerchantSecretKey { get; set; }

        public string MerchantKeyId { get; set; }

        public string KeysDirectory { get; set; }

        public string KeyFilename { get; set; }

        public string RunEnvironment { get; set; }

        public string KeyAlias { get; set; }

        public string KeyPass { get; set; }

        public string EnableLog { get; set; }

        public string LogDirectory { get; set; }

        public string LogFileName { get; set; }

        public string LogFileMaxSize { get; set; }

        public int Timeout { get; set; }

        public string PortfolioID { get; set; }

        public string UseMetaKey { get; set; }

        public string ProxyAddress { get; set; }

        public string ProxyPort { get; set; }

        public bool SendToProduction { get; set; }

        public bool SendToAkamai { get; set; }

        public string ServerURL { get; set; }

        public string CybersourceURL { get; set; }

        public string Password { get; set; }

        public bool Demo { get; set; }

        public int ConnectionLimit { get; set; }

        public bool UseSignedAndEncrypted { get; set; }

        public bool CertificateCacheEnabled { get; set; }

        public string AkamaiTestURL { get; set; }

        public string AkamaiProductionURL { get; set; }

        public string TestURL { get; set; }

        public string ProductionURL { get; set; }

        public string ProductionHost { get; set; }
        #endregion
    }
}
