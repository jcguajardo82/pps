using Jwt;
using Newtonsoft.Json;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Soriana.PPS.Common.Security.JWT
{
    public static class JsonWebTokenHelper
    {
        #region Public Methods
        public static JsonWebTokenResponse CreateJWTCardinal(JsonWebTokenRequest jsonWebTokenRequest)
        {
            if (jsonWebTokenRequest == null) return null;
            Dictionary<string, object> payload = jsonWebTokenRequest.ToDictionary<string, object>();
            return new JsonWebTokenResponse() { JsonWebToken = JsonWebToken.Encode(payload, jsonWebTokenRequest.APIKey, JwtHashAlgorithm.HS256) };
        }

        public static bool TryDecodeJWTCardinal(JsonWebTokenResponse jwtResponse, out JWTCardinalResponse jWTCardinalResponse, out Exception exception)
        {
            jWTCardinalResponse = null;
            exception = null;
            if (jwtResponse == null) return false;
            try
            {
                string jsonPayload = JsonWebToken.Decode(jwtResponse.JsonWebToken, jwtResponse.APIKey);
                jWTCardinalResponse = JsonConvert.DeserializeObject<JWTCardinalResponse>(jsonPayload);
            }
            catch (SignatureVerificationException ex)
            {
                exception = ex;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return true;
        }

        public static bool TryValidJWTCardinal(JsonWebTokenResponse jwtResponse, out Exception exception)
        {
            exception = null;
            if (jwtResponse == null) return false;
            bool isValid = TryDecodeJWTCardinal(jwtResponse, out JWTCardinalResponse jWTCardinalResponse, out Exception ex);
            exception = ex;
            if (!isValid ||
                jWTCardinalResponse == null ||
                jWTCardinalResponse.Payload == null ||
                jWTCardinalResponse.Payload.ErrorNumber != 0)
                return false;
            return isValid;
        }
        #endregion
    }
}
