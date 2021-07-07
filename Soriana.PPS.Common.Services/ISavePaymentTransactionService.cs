using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ISavePaymentTransactionService
    {
        #region Public Methods
        Task InsertPaymentTransactionAsync(PaymentOrderProcessRequest request);
        #endregion
    }
}
