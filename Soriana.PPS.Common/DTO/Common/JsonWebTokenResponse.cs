using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class JsonWebTokenResponse
    {
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.JSON_WEB_TOKEN_RESPONSE, Order = 1)]
        public string JsonWebToken { get; set; }

        //[JsonProperty(JWTCardinalConstants.API_KEY, Order = 2)]
        [JsonIgnore()]
        public string APIKey { get; set; }
        #endregion
    }
}
