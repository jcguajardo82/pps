using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ICallCreateDecisionService
    {
        Task<TransactionResponse> CreateDecisionAsync(PaymentOrderProcessRequest transactionOrderRequest, PaymentOrderProcessRequest paymentOrderRequest);
    }
}
