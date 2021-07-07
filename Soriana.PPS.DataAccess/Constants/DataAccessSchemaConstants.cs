namespace Soriana.PPS.DataAccess.Constants
{
    public static class DataAccessSchemaConstants
    {
        #region Constants TABLE_NAME
        public const string TABLE_NAME_CLIENT_HAS_TOKEN = "tbl_Client_Has_Token";
        #endregion
        #region Constants ERROR_MESSAGE
        public const string ERROR_MESSAGE_SEARCH_FILTER = "Search filter parameter must not be null.";
        #endregion
        #region Constants SQL_STATEMENT
        public const string SQL_STATEMENT_NEXT_VALUE_SEQUENCE = "SELECT NEXT VALUE FOR {0}";
        #endregion
    }
}
