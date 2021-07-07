using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public interface IPaymentTransactionJsonRequestRepository : IRepositoryCreate<PaymentTransactionJsonRequest>, IRepositoryRead<PaymentTransactionJsonRequest>
    {
        Task UpdatePaymentTransactionJsonRequestWithTableAsync(PaymentOrderProcessRequest request);
    }
}
