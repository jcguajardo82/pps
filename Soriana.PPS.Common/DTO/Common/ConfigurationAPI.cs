using System.Collections.Generic;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class ConfigurationAPI : CyberSource.Client.Configuration
    {
        #region Constructors
        public ConfigurationAPI(IReadOnlyDictionary<string, string> merchConfigDictObj) : base(merchConfigDictObj: merchConfigDictObj)
        { }
        #endregion
        #region Public Methods
        public static ConfigurationAPI GetInstance(IReadOnlyDictionary<string, string> merchConfigDictObj)
        {
            return new ConfigurationAPI(merchConfigDictObj);
        }
        #endregion
    }
}
