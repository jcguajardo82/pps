using System.Collections.Generic;

namespace Soriana.PPS.Common.Configuration
{
    public sealed class HttpClientListOptions
    {
        #region Constants
        public const string HTTP_CLIENT_LIST_OPTIONS = "HttpClientListOptions";
        #endregion
        #region Constructors
        public HttpClientListOptions()
        {
            HttpClientOptions = new List<HttpClientOptions>();
        }
        #endregion
        #region Properties
        public IList<HttpClientOptions> HttpClientOptions { get; set; }
        #endregion
    }
}
