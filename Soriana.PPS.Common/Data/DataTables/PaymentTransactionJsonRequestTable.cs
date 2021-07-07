using Soriana.PPS.Common.Constants;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentTransactionJsonRequestTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_TRANSACTION_JSON_REQUEST_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_ID, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_JSON_REQUEST, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = -1;
            dataTable.Columns.Add(dataColumn);
            //Return DataTable
            return dataTable;
        }
        #endregion
    }
}
