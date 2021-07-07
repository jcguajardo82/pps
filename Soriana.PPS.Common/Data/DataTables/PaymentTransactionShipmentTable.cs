using Soriana.PPS.Common.Constants;
using System;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentTransactionShipmentTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_TRANSACTION_SHIPMENT_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_ID, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPMENT_ID_SEQUENCE, typeof(byte));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_ORDER_ID, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_REFERENCE_NUMER, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_REFERENCE_NUMBER, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_STORE_ID, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 10;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_STORE_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 50;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_DELIVERY_ID, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 20;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_DELIVERY_DESC, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 50;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_PAYMENT_INSTALLMENTS, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_PAYMENT_IMPORT, typeof(double));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_FIRST_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_LAST_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ADDRESS, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_CITY, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
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
