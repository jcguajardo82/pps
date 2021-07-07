using CyberSource.Api;
using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication
{
    public sealed class PayerAuthenticationAPI : PayerAuthenticationApi, IPayerAuthenticationAPI
    {
        #region Constructors
        public PayerAuthenticationAPI(ConfigurationAPI configuration) : base(configuration: configuration)
        {

        }
        #endregion
    }
}
