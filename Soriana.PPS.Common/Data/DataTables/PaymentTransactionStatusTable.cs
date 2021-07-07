using Soriana.PPS.Common.Constants;
using System;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentTransactionStatusTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_TRANSACTION_STATUS_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_ID, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_SERVICE, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.TRANSACTION_STATUS, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.IS_RETRYING, typeof(bool));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.AFFILIATION_TYPE_ID, typeof(short));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.IS_ACTIVE, typeof(bool));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CREATED_DATE, typeof(DateTime));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.UPDATED_DATE, typeof(DateTime));
            dataColumn.AllowDBNull = true;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.DELETED_DATE, typeof(DateTime));
            dataColumn.AllowDBNull = true;
            dataTable.Columns.Add(dataColumn);
            //Return DataTable
            return dataTable;
        }
        #endregion
    }
}
