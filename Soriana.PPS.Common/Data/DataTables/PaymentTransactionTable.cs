using Soriana.PPS.Common.Constants;
using System;
using System.Data;

namespace Soriana.PPS.Common.Data.DataTables
{
    public sealed class PaymentTransactionTable : ICreateTable
    {
        #region Public Methods
        public DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TableValuedParameterConstants.PAYMENT_TRANSACTION_TABLE_TYPE);
            //Definition Column
            DataColumn dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TRANSACTION_ID, typeof(long));
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
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_DATE, typeof(DateTime));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_TIME, typeof(TimeSpan));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.MERCHANTDISE_TYPE, typeof(byte));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_SALE_CHANNEL, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 10;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_COUPON_CODE, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 20;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.ORDER_AMOUNT, typeof(double));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_EMAIL, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 50;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_FIRST_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_LAST_NAME, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 30;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_ID, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 12;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_DEVICE_FINGER_PRINT_ID, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 50;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_IP_ADDRESS, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 20;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_PURCHASES_QUANTITY, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_CONTACT, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 16;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_LOTALTY_CARD_ID, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 20;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_REGISTERED_DAYS, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.RETURN_URL, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TYPE, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 15;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_PROCESSOR, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 15;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_TOKEN, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 60;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_SAVE_CARD, typeof(bool));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_CARD_CVV, typeof(short));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.PAYMENT_CARD_NIP, typeof(short));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_ADDRESS, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_CITY, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 255;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_STATE, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 50;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_ZIP_CODE, typeof(int));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_COUNTRY, typeof(string));
            dataColumn.AllowDBNull = false;
            dataColumn.MaxLength = 10;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_MONEY, typeof(double));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_ELECTRONIC_MONEY, typeof(double));
            dataColumn.AllowDBNull = false;
            dataTable.Columns.Add(dataColumn);
            //Definition Column
            dataColumn = new DataColumn(ColumnNameConstants.CUSTOMER_LOYALTY_REDEEM_POINTS, typeof(double));
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
