using CyberSource.Model;
using Soriana.PPS.Common.Enums;

namespace Soriana.PPS.Common.DTO.Cybersource.Payments
{
    public sealed class PaymentsRequest : CreatePaymentRequest
    {
        #region Public Properties
        public AffiliationTypeEnum AffiliationType { get; set; }

        public MerchandiseTypeEnum MerchandiseType { get; set; }

        public bool IsRetrying { get; set; }
        #endregion
    }
}
