using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class OrdenJWTCardinalRequest
    {
        #region Constructors
        public OrdenJWTCardinalRequest()
        {
            OrdenDetails = new OrdenDetailsJWTCardinalRequest();
        }
        #endregion
        #region Public Methods
        [JsonProperty(JWTCardinalConstants.ORDEN_DETAILS, Order = 1)]
        [SourceNames(true, JWTCardinalConstants.ORDEN_DETAILS)]
        public OrdenDetailsJWTCardinalRequest OrdenDetails { get; set; }
        #endregion
    }
}
