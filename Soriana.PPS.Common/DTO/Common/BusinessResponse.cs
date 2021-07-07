using Newtonsoft.Json;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class BusinessResponse
    {
        #region Public Properties
        [JsonProperty("statuscode", Order = 1)]
        public int StatusCode { get; set; }

        [JsonProperty("description", Order = 2)]
        public string Description { get; set; }

        [JsonProperty("descriptiondetail", Order = 3)]
        public object DescriptionDetail { get; set; }

        [JsonProperty("contentrequest", Order = 4)]
        public object ContentRequest { get; set; }

        [JsonProperty("contentresponse", Order = 5)]
        public object ContentResponse { get; set; }
        #endregion
    }
}
