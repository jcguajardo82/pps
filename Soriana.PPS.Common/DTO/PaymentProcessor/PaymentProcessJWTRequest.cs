using Newtonsoft.Json;
using Soriana.PPS.Common.Security.JWT;

namespace Soriana.PPS.Common.DTO.PaymentProcessor
{
    public sealed class PaymentProcessJWTRequest : JWTCardinalRequest
    {
        /// <summary>
        /// TODO: Pendiente Validar los nombres de los campos e información que deben contener
        /// para generar debidamente el JWT
        /// </summary>
        #region Public Properties
        [JsonProperty("Data1", Order = 1)]
        public string Data1 { get; set; }

        [JsonProperty("Data2", Order = 1)]
        public string Data2 { get; set; }

        [JsonProperty("Data3", Order = 1)]
        public string Data3 { get; set; }

        [JsonProperty("Data4", Order = 1)]
        public string Data4 { get; set; }

        [JsonProperty("Data5", Order = 1)]
        public string Data5 { get; set; }
        #endregion
    }
}
