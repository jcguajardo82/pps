using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_TYPE_NAME_PAYMENT_ORDER_JSON_REQUEST)]
    public sealed class PaymentOrderJsonRequestTableType
    {
        [SourceNames(ColumnNameConstants.ORDER_REFERENCE_NUMER)]
        public long OrderReferenceNumber { get; set; }

        [SourceNames(ColumnNameConstants.PAYMENT_ORDER_JSON_REQUEST)]
        public string PaymentOrderJSONRequest { get; set; }
    }
}
