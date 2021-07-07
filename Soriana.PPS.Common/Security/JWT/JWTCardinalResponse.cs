using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class JWTCardinalResponse
    {
        #region Constructors
        public JWTCardinalResponse()
        {
            Payload = new PayloadJWTCardinalResponse();
        }
        #endregion
        #region Public Properties
        /// <summary>
        /// Expiration - The numeric epoch time that the JWT should be consider expired. 
        /// This value is ignored if its larger than 2hrs. 
        /// By default we will consider any JWT older than 2hrs.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.EXPIRATION, Order = 1)]
        [SourceNames(JWTCardinalConstants.EXPIRATION)]
        public string Expiration { get; set; }

        /// <summary>
        /// Issued At - The epoch time in seconds of when the JWT was generated. 
        /// This allows us to determine how long a JWT has been around and whether we consider it expired or not.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.ISSUED_AT, Order = 2)]
        [SourceNames(JWTCardinalConstants.ISSUED_AT)]
        public string IssuedAt { get; set; }

        /// <summary>
        /// Issuer - An identifier of who is issuing the JWT. 
        /// We use this value to contain the Api Key identifier or name.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.ISSUER, Order = 3)]
        [SourceNames(JWTCardinalConstants.ISSUER)]
        public string Issuer { get; set; }

        /// <summary>
        /// JWT Id - A unique identifier for this JWT. 
        /// This field should change each time a JWT is generated.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.JWT_ID, Order = 4)]
        [SourceNames(JWTCardinalConstants.JWT_ID)]
        public string JWTId { get; set; }

        /// <summary>
        /// The unique session Id for the current user.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.CONSUMER_SESSION_ID, Order = 5)]
        [SourceNames(JWTCardinalConstants.CONSUMER_SESSION_ID)]
        public string ConsumerSessionId { get; set; }

        /// <summary>
        /// The JSON data object being sent to Cardinal. This object is usually an Order object.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.PAYLOAD, Order = 6)]
        [SourceNames(true, JWTCardinalConstants.PAYLOAD)]
        public PayloadJWTCardinalResponse Payload { get; set; }
        #endregion
    }
}
