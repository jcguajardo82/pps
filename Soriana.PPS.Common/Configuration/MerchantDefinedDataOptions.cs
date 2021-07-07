using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Configuration
{
    public sealed class MerchantDefinedDataOptions
    {
        #region Public Constants
        public const string MERCHANT_DEFINED_DATA_OPTIONS_CONFIGURATION = "MerchantDefinedDataOptions";
        #endregion
        #region Public Properties
        [JsonProperty(ConfigurationConstants.GROCERY_CONFIGURATION_NUMBER, Order = 1)]
        [SourceNames(ConfigurationConstants.GROCERY_CONFIGURATION_NUMBER)]
        public byte GroceryConfigurationNumber { get; set; }

        [JsonProperty(ConfigurationConstants.NON_GROCERY_CONFIGURATION_NUMBER, Order = 2)]
        [SourceNames(ConfigurationConstants.NON_GROCERY_CONFIGURATION_NUMBER)]
        public byte NonGroceryConfigurationNumber { get; set; }
        #endregion
    }
}
