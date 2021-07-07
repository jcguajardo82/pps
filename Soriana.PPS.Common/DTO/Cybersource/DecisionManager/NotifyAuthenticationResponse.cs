namespace Soriana.PPS.Common.DTO.Cybersource.DecisionManager
{
    public sealed class NotifyAuthenticationResponse
    {
        #region Public Properties
        public string ReasonCode { get; set; }

        public string RequestID { get; set; }

        public string AuthorizationCode { get; set; }

        public string CaptureRequestTime { get; set; }

        public string CapturedAmount { get; set; }

        public bool HasError { get; set; } = false;

        public string ErrorDetails { get; set; }
        #endregion
    }
}
