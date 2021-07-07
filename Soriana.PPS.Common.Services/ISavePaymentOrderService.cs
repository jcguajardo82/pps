using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ISavePaymentOrderService
    {
        #region Public Methods
        Task InsertPaymentOrderAsync(PaymentOrderProcessRequest request);
        #endregion
    }
}
