using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface IUpdatePaymentTransactionJsonRequestService
    {
        Task UpdatePaymentTransactionJsonRequestAsync(PaymentOrderProcessRequest request);
    }
}
