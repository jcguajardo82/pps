namespace Soriana.OMS.Ordenes.Common.Configuration
{
    public class DatabaseOptions
    {
        #region Public Properties
        public string ConnectionString { get; set; }

        public bool IsConnectionStringEncrypted { get; set; } = false;
        #endregion
    }
}
