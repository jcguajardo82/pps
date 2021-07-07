using CyberSource.Api;
using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.DTO.Cybersource.Payments
{
    public sealed class CapturesAPI : CaptureApi, ICapturesAPI
    {
        #region Constructors
        public CapturesAPI(ConfigurationAPI configuration) : base(configuration: configuration)
        {

        }
        #endregion
    }
}
