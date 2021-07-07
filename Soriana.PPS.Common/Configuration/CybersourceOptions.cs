using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;
using System.Collections.Generic;

namespace Soriana.PPS.Common.Configuration
{
    public sealed class CybersourceOptions
    {
        #region Public Constants
        public const string CYBERSOURCE_OPTIONS_CONFIGURATION = "CybersourceOptions";
        #endregion
        #region Public Properties
        [JsonProperty(ConfigurationConstants.AUTHENTICATION_TYPE, Order = 1)]
        [SourceNames(ConfigurationConstants.AUTHENTICATION_TYPE)]
        public string AuthenticationType { get; set; }

        [JsonProperty(ConfigurationConstants.MERCHANT_ID, Order = 2)]
        [SourceNames(ConfigurationConstants.MERCHANT_ID)]
        public string MerchantID { get; set; }

        [JsonProperty(ConfigurationConstants.MERCHANT_SECRET_KEY, Order = 3)]
        [SourceNames(ConfigurationConstants.MERCHANT_SECRET_KEY)]
        public string MerchantSecretKey { get; set; }

        [JsonProperty(ConfigurationConstants.MERCHANT_KEY_ID, Order = 4)]
        [SourceNames(ConfigurationConstants.MERCHANT_KEY_ID)]
        public string MerchantKeyId { get; set; }

        [JsonProperty(ConfigurationConstants.KEYS_DIRECTORY, Order = 5)]
        [SourceNames(ConfigurationConstants.KEYS_DIRECTORY)]
        public string KeysDirectory { get; set; }

        [JsonProperty(ConfigurationConstants.KEY_FILENAME, Order = 6)]
        [SourceNames(ConfigurationConstants.KEY_FILENAME)]
        public string KeyFilename { get; set; }

        [JsonProperty(ConfigurationConstants.RUN_ENVIRONMENT, Order = 7)]
        [SourceNames(ConfigurationConstants.RUN_ENVIRONMENT)]
        public string RunEnvironment { get; set; }

        [JsonProperty(ConfigurationConstants.KEY_ALIAS, Order = 8)]
        [SourceNames(ConfigurationConstants.KEY_ALIAS)]
        public string KeyAlias { get; set; }

        [JsonProperty(ConfigurationConstants.KEY_PASS, Order = 9)]
        [SourceNames(ConfigurationConstants.KEY_PASS)]
        public string KeyPass { get; set; }

        [JsonProperty(ConfigurationConstants.ENABLE_LOG, Order = 10)]
        [SourceNames(ConfigurationConstants.ENABLE_LOG)]
        public string EnableLog { get; set; }

        [JsonProperty(ConfigurationConstants.LOG_DIRECTORY, Order = 11)]
        [SourceNames(ConfigurationConstants.LOG_DIRECTORY)]
        public string LogDirectory { get; set; }

        [JsonProperty(ConfigurationConstants.LOG_FILENAME, Order = 12)]
        [SourceNames(ConfigurationConstants.LOG_FILENAME)]
        public string LogFileName { get; set; }

        [JsonProperty(ConfigurationConstants.LOG_FILE_MAX_SIZE, Order = 13)]
        [SourceNames(ConfigurationConstants.LOG_FILE_MAX_SIZE)]
        public string LogFileMaxSize { get; set; }

        [JsonProperty(ConfigurationConstants.TIMEOUT, Order = 14)]
        [SourceNames(ConfigurationConstants.TIMEOUT)]
        public string Timeout { get; set; }

        [JsonProperty(ConfigurationConstants.PORT_FOLIO_ID, Order = 15)]
        [SourceNames(ConfigurationConstants.PORT_FOLIO_ID)]
        public string PortfolioID { get; set; }

        [JsonProperty(ConfigurationConstants.USE_META_KEY, Order = 16)]
        [SourceNames(ConfigurationConstants.USE_META_KEY)]
        public string UseMetaKey { get; set; }

        [JsonProperty(ConfigurationConstants.PROXY_ADDRESS, Order = 17)]
        [SourceNames(ConfigurationConstants.PROXY_ADDRESS)]
        public string ProxyAddress { get; set; }

        [JsonProperty(ConfigurationConstants.PROXY_PORT, Order = 18)]
        [SourceNames(ConfigurationConstants.PROXY_PORT)]
        public string ProxyPort { get; set; }

        [JsonProperty(ConfigurationConstants.AFFILIATION_TYPE, Order = 19)]
        [SourceNames(ConfigurationConstants.AFFILIATION_TYPE)]
        public string AffiliationType { get; set; }

        #endregion
        #region Public Methods
        public Dictionary<string, string> GetCybersourceConfiguration()
        {
            Dictionary<string, string> keyValuePairsConfig = new Dictionary<string, string>();
            keyValuePairsConfig.Add("authenticationType", AuthenticationType);
            keyValuePairsConfig.Add("merchantID", MerchantID);
            keyValuePairsConfig.Add("merchantsecretKey", MerchantSecretKey);
            keyValuePairsConfig.Add("merchantKeyId", MerchantKeyId);
            keyValuePairsConfig.Add("keysDirectory", KeysDirectory);
            keyValuePairsConfig.Add("keyFilename", KeyFilename);
            keyValuePairsConfig.Add("runEnvironment", RunEnvironment);
            keyValuePairsConfig.Add("keyAlias", MerchantID);
            keyValuePairsConfig.Add("keyPass", MerchantID);
            keyValuePairsConfig.Add("enableLog", string.IsNullOrEmpty(EnableLog) ? string.Empty : EnableLog.ToUpper());
            keyValuePairsConfig.Add("logDirectory", LogDirectory);
            keyValuePairsConfig.Add("logFileName", LogFileName);
            keyValuePairsConfig.Add("logFileMaxSize", LogFileMaxSize);
            keyValuePairsConfig.Add("timeout", Timeout);
            keyValuePairsConfig.Add("portfolioID", PortfolioID);
            keyValuePairsConfig.Add("useMetaKey", string.IsNullOrEmpty(UseMetaKey) ? string.Empty : UseMetaKey.ToLower());
            keyValuePairsConfig.Add("proxyAddress", ProxyAddress);
            keyValuePairsConfig.Add("proxyPort", ProxyPort);
            return keyValuePairsConfig;
        }
        #endregion
    }
}
