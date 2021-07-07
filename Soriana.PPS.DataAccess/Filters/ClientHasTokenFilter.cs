using Soriana.PPS.Common.DTO.Filters;

namespace Soriana.PPS.DataAccess.Filters
{
    public sealed class ClientHasTokenFilter : SearchFilterBase, ISearchFilter
    {
        #region Public Properties
        public long ClientID { get; set; }
        public string CustomerID { get; set; }
        public string ClientToken { get; set; }
        #endregion
    }
}
