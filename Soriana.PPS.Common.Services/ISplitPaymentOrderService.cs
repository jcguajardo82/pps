using Soriana.PPS.Common.DTO.Salesforce;

namespace Soriana.PPS.Common.Services
{
    public interface ISplitPaymentOrderService
    {
        void SplitPaymentOrderByMerchandiseType(PaymentOrderProcessRequest paymentOrderProcessRequest, out PaymentOrderProcessRequest paymentOrderProcessRequestGrocery, out PaymentOrderProcessRequest paymentOrderProcessRequestNonGrocery);
    }
}
