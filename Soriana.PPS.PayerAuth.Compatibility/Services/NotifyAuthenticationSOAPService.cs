using Microsoft.Extensions.Options;
using Soriana.PPS.DecisionManager.Common.Compatibility.Configuration;
using Soriana.PPS.DecisionManager.Common.Compatibility.Constants;
using Soriana.PPS.DecisionManager.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.Common.Compatibility.Services
{
    public class NotifyAuthenticationSOAPService : INotifyAuthenticationSOAPService
    {

        #region Private Fields
        private readonly CybersourceOptions _CybersourceOptions;
        #endregion
        #region Constructors
        public NotifyAuthenticationSOAPService(IOptions<CybersourceOptions> options)
        {
            ValidateOptions(options);
            _CybersourceOptions = options.Value;
        }
        #endregion
        #region Public Methods
        public async Task<string> NotifyAuthenticationAsync(dynamic notifyAuthenticationRequest)
        {
            NVPTransactionProcessorClientHelper.SetEffectiveVariables(_CybersourceOptions);
            return await NVPTransactionProcessorClientHelper.RunTransaction(_CybersourceOptions, notifyAuthenticationRequest);
        }
        #endregion
        #region Private Methods
        private void ValidateOptions(IOptions<CybersourceOptions> cyberSouceOptions)
        {
            if (cyberSouceOptions == null || cyberSouceOptions.Value == null)
                throw new Exception(NotifyAuthenticationConstants.CONFIGURATION_BUG_CYBERSOURCE_OPTIONS);
        }
        #endregion
    }
}