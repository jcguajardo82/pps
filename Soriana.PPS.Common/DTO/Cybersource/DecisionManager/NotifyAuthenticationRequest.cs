namespace Soriana.PPS.Common.DTO.Cybersource.DecisionManager
{
    public class NotifyAuthenticationRequest
    {
        #region Public Properties
        public string MerchantReferenceCode { get; set; }

        public string ActionCode { get; set; }

        public string RequestID { get; set; }

        public string Comments { get; set; }
        #endregion
    }
}
