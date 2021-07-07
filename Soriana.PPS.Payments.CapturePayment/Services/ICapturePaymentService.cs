using Soriana.PPS.Common.DTO.Cybersource.Payments;
using System.Threading.Tasks;

namespace Soriana.PPS.Payments.CapturePayment.Services
{
    public interface ICapturePaymentService
    {
        Task<CapturesPayments201Response> CapturePayment(CapturesPaymentsRequest capturesPaymentsRequest);
    }
}
