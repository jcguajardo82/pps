using System.Collections.Generic;

namespace Soriana.PPS.Common.Configuration
{
    public sealed class CybersourceListOptions
    {
        #region Constants
        public const string CYBERSOURCE_LIST_OPTIONS_CONFIGURATION = "CybersourceListOptions";
        #endregion
        #region Constructors
        public CybersourceListOptions()
        {
            CybersourceOptions = new List<CybersourceOptions>();
        }
        #endregion
        #region Properties
        public IList<CybersourceOptions> CybersourceOptions { get; set; }
        #endregion
    }
}
