using Soriana.PPS.Common.Constants;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentOrderJsonRequestTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_ORDER_JSON_REQUEST_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.ORDER_REFERENCE_NUMER, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_ORDER_JSON_REQUEST, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = -1;
            dataTable.Columns.Add(dataColumn);
            //Return DataTable
            return dataTable;
        }
        #endregion
    }
}
