using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.PaymentOrderProcess.Services
{
    public interface IPaymentOrderProcessService
    {
        Task<PaymentOrderProcessResponse> PaymentOrderProcess(PaymentOrderProcessRequest paymentOrderProcessRequest);
    }
}
