using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ICallProcessPaymentService
    {
        Task<TransactionResponse> ProcessPaymentAsync(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest);
    }
}
