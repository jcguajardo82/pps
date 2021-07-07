using CyberSource.Model;

namespace Soriana.PPS.Common.DTO.Cybersource.Payments
{
    public sealed class CapturesPaymentsRequest : CapturePaymentRequest
    {
        #region Public Methods
        public string TransactionAuthorizationId { get; set; }
        #endregion
    }
}
