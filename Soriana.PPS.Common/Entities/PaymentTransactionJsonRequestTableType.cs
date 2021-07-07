using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_TRANSACTION_JSON_REQUEST)]
    public sealed class PaymentTransactionJsonRequestTableType
    {
        [SourceNames(ColumnNameConstants.PAYMENT_TRANSACTION_ID)]
        public long PaymentTransactionID { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_TRANSACTION_JSON_REQUEST)]
        public string PaymentTransactionJSONRequest { get; set; }
    }
}
