using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_TRANSACTION_STATUS)]
    public sealed class PaymentTransactionStatusTableType : EntityBase
    {
        [SourceNames(ColumnNameConstants.PAYMENT_TRANSACTION_ID)]
        public long PaymentTransactionID { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_TRANSACTION_SERVICE)]
        public string PaymentTransactionService { get; set; }

        [SourceNames(ColumnNameConstants.TRANSACTION_STATUS)]
        public string TransactionStatus { get; set; }

        [SourceNames(ColumnNameConstants.IS_RETRYING)]
        public bool IsRetrying { get; set; }

        [SourceNames(ColumnNameConstants.AFFILIATION_TYPE_ID)]
        public byte AffiliationTypeID { get; set; }
    }
}
