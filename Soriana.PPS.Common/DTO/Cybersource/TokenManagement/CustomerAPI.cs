using CyberSource.Api;
using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.DTO.Cybersource.TokenManagement
{
    public sealed class CustomerAPI : CustomerApi, ICustomerAPI
    {
        #region Constructors
        public CustomerAPI(ConfigurationAPI configuration) : base(configuration: configuration)
        {

        }
        #endregion
    }
}
