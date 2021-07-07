using CyberSource.Model;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Cybersource.DecisionManager
{
    public sealed class DecisionManager201Response : RiskV1DecisionsPost201Response
    {
        #region Public Properties
        [JsonProperty(propertyName: JsonFieldNamesConstants.DECISION_MANAGER_PROFILE, Order = 1)]
        public DecisionManagerCustomProfile Profile { get; set; }
        #endregion
    }
}
