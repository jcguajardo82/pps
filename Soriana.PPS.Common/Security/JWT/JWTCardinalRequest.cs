using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;

namespace Soriana.PPS.Common.Security.JWT
{
    public class JWTCardinalRequest
    {
        #region Constructors
        public JWTCardinalRequest()
        {
            Payload = new PayloadJWTCardinalRequest();
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
        /// Audience - Cardinal populates this field on response JWT to contain the request jti field. 
        /// This allows merchant to match up request JWTs with response JWTs.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.AUDIENCE, Order = 5)]
        [SourceNames(JWTCardinalConstants.AUDIENCE)]
        public string Audience { get; set; }

        /// <summary>
        /// The merchant SSO OrgUnitId
        /// </summary>
        [JsonProperty(JWTCardinalConstants.ORGANIZATION_UNIT_ID, Order = 6)]
        [SourceNames(JWTCardinalConstants.ORGANIZATION_UNIT_ID)]
        public string OrganizationUnitId { get; set; }

        /// <summary>
        /// The JSON data object being sent to Cardinal. This object is usually an Order object.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.PAYLOAD, Order = 7)]
        [SourceNames(isCustomClass: true, isCustomClassSerializedAsJson: true, concatInnerPropertyName: false, JWTCardinalConstants.PAYLOAD)]
        public PayloadJWTCardinalRequest Payload { get; set; }

        /// <summary>
        /// This is a merchant supplied identifier that can be used to match up data between Device Fingerprinter and Centinel. 
        /// Centinel can then use data collected by Device Fingerprinter to run decisions on like XML rules to affect the results of transactions.
        /// This field is Required, unless merchant decides to use the SessionId returned from Songbird or Device Data Collection url.
        /// </summary>
        [JsonProperty(JWTCardinalConstants.REFERENCE_ID, Order = 8)]
        [SourceNames(JWTCardinalConstants.REFERENCE_ID)]
        public string ReferenceId { get; set; }
        #endregion
    }
}