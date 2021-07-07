using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class PaymentJWTCardinalResponse
    {
        #region Constructors
        public PaymentJWTCardinalResponse()
        {
            ExtendedData = new ExtendedDataJWTCardinalResponse();
        }
        #endregion
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.TYPE, Order = 1)]
        [SourceNames(JWTCardinalConstants.TYPE)]
        public string Type { get; set; }

        [JsonProperty(JWTCardinalConstants.EXTENDED_DATA, Order = 2)]
        [SourceNames(true, JWTCardinalConstants.EXTENDED_DATA)]
        public ExtendedDataJWTCardinalResponse ExtendedData { get; set; }
        #endregion
    }
}
