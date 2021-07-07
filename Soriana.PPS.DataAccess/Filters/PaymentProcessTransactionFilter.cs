using Soriana.PPS.Common.DTO.Filters;

namespace Soriana.PPS.DataAccess.Filters
{
    public sealed class PaymentProcessTransactionFilter : SearchFilterBase, ISearchFilter
    {
        #region Public Properties
        public long PaymentProcessTransactionID { get; set; }

        public long ParentPaymentProcessTransactionID { get; set; }
        #endregion
    }
}
