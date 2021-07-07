using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class PayloadJWTCardinalRequest
    {
        #region Constructors
        public PayloadJWTCardinalRequest()
        {
            OrdenDetails = new OrdenDetailsJWTCardinalRequest();
        }
        #endregion
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.ORDEN_DETAILS, Order = 1)]
        [SourceNames(true, JWTCardinalConstants.ORDEN_DETAILS)]
        public OrdenDetailsJWTCardinalRequest OrdenDetails { get; set; }
        #endregion
    }
}
