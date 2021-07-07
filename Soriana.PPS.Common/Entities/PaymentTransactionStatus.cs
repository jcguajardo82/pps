using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_NAME_PAYMENT_TRANSACTION)]
    public sealed class PaymentTransactionStatus : EntityBase
    {
        #region Public Properties
        [ExplicitKey()]
        public long PaymentTransactionID { get; set; }
        [ExplicitKey()]
        public byte TransactionStatusIDSequence { get; set; }
        public string PaymentTransactionService { get; set; }
        public string TransactionStatus { get; set; }
        public bool IsRetrying { get; set; }
        public byte AffiliationTypeID { get; set; }
        #endregion
    }
}
