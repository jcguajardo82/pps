using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ICallEnrollmentService
    {
        Task<TransactionResponse> EnrollmentAsync(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest);
    }
}
