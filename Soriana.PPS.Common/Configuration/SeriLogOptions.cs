namespace Soriana.PPS.Common.Configuration
{
    public sealed class SeriLogOptions
    {
        #region Constants
        public const string SERILOG_OPTIONS_CONFIG_SECTION = "SeriLogOptions";
        #endregion
        #region Public Properties
        public string LogFilePath { get; set; }
        #endregion
    }
}
