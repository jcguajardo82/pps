using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ISetTransactionReferenceService
    {
        Task SetTransactionReferenceAsync(PaymentOrderProcessRequest request);
    }
}
