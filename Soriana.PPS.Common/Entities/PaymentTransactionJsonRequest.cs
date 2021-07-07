using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_NAME_PAYMENT_TRANSACTION_JSON_REQUEST)]
    public sealed class PaymentTransactionJsonRequest
    {
        #region Public Properties
        [Key()]
        public long PaymentTransactionID { get; set; }

        public string PaymentTransactionJSONRequest { get; set; }
        #endregion
    }
}
