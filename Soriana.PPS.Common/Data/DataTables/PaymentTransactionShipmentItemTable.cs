using Soriana.PPS.Common.Constants;
using System;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentTransactionShipmentItemTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_TRANSACTION_SHIPMENT_ITEM_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_ID, typeof(long));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPMENT_ID_SEQUENCE, typeof(byte));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ITEM_ID_SEQUENCE, typeof(byte));
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
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_ID, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_EAN, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_CATEGORY, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_PRICE, typeof(double));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_QUANTITY, typeof(byte));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.SHIPPING_ITEM_TOTAL, typeof(double));
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
