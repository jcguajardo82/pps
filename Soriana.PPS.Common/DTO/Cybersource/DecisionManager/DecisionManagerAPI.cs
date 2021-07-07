using CyberSource.Api;
using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.DTO.Cybersource.DecisionManager
{
    public sealed class DecisionManagerAPI : DecisionManagerApi, IDecisionManagerAPI
    {
        #region Constructors
        public DecisionManagerAPI(ConfigurationAPI configuration) : base(configuration: configuration)
        {

        }
        #endregion
    }
}
