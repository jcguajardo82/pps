using Soriana.PPS.Common.DTO.PaymentProcessor;
using System.Threading.Tasks;

namespace Soriana.PPS.Control.GenerateTransactionReference.Services
{
    public interface IGenerateTransactionReferenceService
    {
        Task<TransactionReferenceResponse> GenerateTransactionReference();
    }
}
