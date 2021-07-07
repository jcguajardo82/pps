using Soriana.PPS.Common.DTO.Cybersource.Payments;
using System.Threading.Tasks;

namespace Soriana.PPS.Payments.ProcessPayment.Services
{
    public interface IProcessPaymentService
    {
        Task<Payments201Response> ProcessPayment(PaymentsRequest paymentRequest);
    }
}
