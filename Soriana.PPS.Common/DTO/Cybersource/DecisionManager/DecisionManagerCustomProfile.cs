using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.DTO.Cybersource.DecisionManager
{
    public sealed class DecisionManagerCustomProfile
    {
        #region Public Properties
        [JsonProperty(propertyName: JsonFieldNamesConstants.DECISION_MANAGER_PROFILE_EARLY_DECISION, Order = 1)]
        public string EarlyDecision { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.DECISION_MANAGER_PROFILE_DESTINATION_QUEUE, Order = 2)]
        public string DestinationQueue { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.DECISION_MANAGER_PROFILE_NAME, Order = 3)]
        public string Name { get; set; }

        [JsonProperty(propertyName: JsonFieldNamesConstants.DECISION_MANAGER_PROFILE_SELECTOR_RULE, Order = 4)]
        public string SelectorRule { get; set; }
        #endregion
    }
}
