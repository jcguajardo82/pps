namespace Soriana.PPS.Common.Configuration
{
    public sealed class HttpClientOptions
    {
        #region Constants
        public const string HTTP_CLIENT_OPTIONS = "HttpClientOptions";
        #endregion
        #region Public Properties
        public string EndPoint { get; set; }
        public string EndPointName { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string ContentType { get; set; }
        #endregion
    }
}
