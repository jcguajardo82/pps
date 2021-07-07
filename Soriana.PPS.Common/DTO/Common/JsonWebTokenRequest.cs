using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Security.JWT;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class JsonWebTokenRequest : JWTCardinalRequest
    {
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.API_KEY, Order = 1)]
        public string APIKey { get; set; }

        [JsonProperty(JWTCardinalConstants.API_IDENTIFIER, Order = 2)]
        public string APIIdentifier { get; set; }
        #endregion
    }
}
