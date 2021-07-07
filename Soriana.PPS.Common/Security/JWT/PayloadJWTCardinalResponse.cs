using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class PayloadJWTCardinalResponse
    {
        #region Constructors
        public PayloadJWTCardinalResponse()
        {

        }
        #endregion
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.VALIDATED, Order = 1)]
        [SourceNames(JWTCardinalConstants.VALIDATED)]
        public bool Validated { get; set; }

        [JsonProperty(JWTCardinalConstants.PAYMENT, Order = 2)]
        [SourceNames(true, JWTCardinalConstants.PAYMENT)]
        public PaymentJWTCardinalResponse Payment { get; set; }

        [JsonProperty(JWTCardinalConstants.ACTION_CODE, Order = 3)]
        [SourceNames(JWTCardinalConstants.ACTION_CODE)]
        public string ActionCode { get; set; }

        [JsonProperty(JWTCardinalConstants.ERROR_NUMBER, Order = 4)]
        [SourceNames(JWTCardinalConstants.ERROR_NUMBER)]
        public int ErrorNumber { get; set; }

        [JsonProperty(JWTCardinalConstants.ERROR_DESCRIPTION, Order = 5)]
        [SourceNames(JWTCardinalConstants.ERROR_DESCRIPTION)]
        public string ErrorDescription { get; set; }
        #endregion
    }
}
