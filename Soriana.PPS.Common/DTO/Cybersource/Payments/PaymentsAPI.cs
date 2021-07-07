using CyberSource.Api;
using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.DTO.Cybersource.Payments
{
    public sealed class PaymentsAPI : PaymentsApi, IPaymentsAPI
    {
        #region Constructors
        public PaymentsAPI(ConfigurationAPI configuration) : base(configuration: configuration)
        {

        }
        #endregion
    }
}
