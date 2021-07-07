using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public sealed class OrdenDetailsJWTCardinalRequest
    {
        #region Public Properties
        [JsonProperty(JWTCardinalConstants.ORDEN_DETAILS_ORDER_NUMBER, Order = 1)]
        [SourceNames(JWTCardinalConstants.ORDEN_DETAILS_ORDER_NUMBER)]
        public string OrderNumber { get; set; }

        [JsonProperty(JWTCardinalConstants.ORDEN_DETAILS_AMOUNT, Order = 2)]
        [SourceNames(JWTCardinalConstants.ORDEN_DETAILS_AMOUNT)]
        public string Amount { get; set; }

        [JsonProperty(JWTCardinalConstants.ORDEN_DETAILS_CURRENCY_CODE, Order = 3)]
        [SourceNames(JWTCardinalConstants.ORDEN_DETAILS_CURRENCY_CODE)]
        public string CurrencyCode { get; set; }
        #endregion
    }
}
