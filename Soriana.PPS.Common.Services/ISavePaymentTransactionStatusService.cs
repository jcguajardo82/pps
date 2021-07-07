using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ISavePaymentTransactionStatusService
    {
        #region Public Methods
        Task InsertPaymentTransactionStatusAsync(PaymentOrderProcessRequest request);
        #endregion
    }
}
