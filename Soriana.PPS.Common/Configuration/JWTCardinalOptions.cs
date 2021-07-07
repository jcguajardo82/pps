namespace Soriana.PPS.Common.Configuration
{
    public class JWTCardinalOptions
    {
        #region Constants
        public const string JWT_CARDINAL_OPTION_CONFIG_SECTION = "JWTCardinalOptions";
        #endregion
        #region Public Properties
        public string OrganizationUnitId { get; set; }
        public string APIIdentifier { get; set; }
        public string APIKey { get; set; }
        public int ExpirationTimeMinutes { get; set; }
        #endregion
    }
}
