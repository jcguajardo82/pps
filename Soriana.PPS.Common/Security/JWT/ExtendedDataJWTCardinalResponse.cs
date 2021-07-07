using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class ExtendedDataJWTCardinalResponse
    {
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.CAVV, Order = 1)]
        [SourceNames(JWTCardinalConstants.CAVV)]
        public string CAVV { get; set; }

        [JsonProperty(JWTCardinalConstants.ECI_FLAG, Order = 2)]
        [SourceNames(JWTCardinalConstants.ECI_FLAG)]
        public string ECIFlag { get; set; }

        [JsonProperty(JWTCardinalConstants.PARES_STATUS, Order = 3)]
        [SourceNames(JWTCardinalConstants.PARES_STATUS)]
        public string PAResStatus { get; set; }

        [JsonProperty(JWTCardinalConstants.SIGNATURE_VERIFICATION, Order = 4)]
        [SourceNames(JWTCardinalConstants.SIGNATURE_VERIFICATION)]
        public string SignatureVerification { get; set; }

        [JsonProperty(JWTCardinalConstants.XID, Order = 5)]
        [SourceNames(JWTCardinalConstants.XID)]
        public string XID { get; set; }

        [JsonProperty(JWTCardinalConstants.ENROLLED, Order = 6)]
        [SourceNames(JWTCardinalConstants.ENROLLED)]
        public string Enrolled { get; set; }
        #endregion
    }
}
