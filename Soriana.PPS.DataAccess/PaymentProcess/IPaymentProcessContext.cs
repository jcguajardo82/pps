using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.DataAccess.Filters;
using Soriana.PPS.Common.DTO.ClosureOrder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.PaymentProcess
{
    public interface IPaymentProcessContext
    {
        #region Public Methods
        Task<long> GetPaymentTransactionIDAsync();
        Task<PaymentTransaction> GetPaymentTransactionAsync(PaymentProcessTransactionFilter filter);
        Task InsertPaymentTransactionAsync(PaymentTransaction entity);
        Task InsertClientHasTokenAsync(ClientHasToken entity);
        Task<IList<ItemResult>> GetNonGroceryItems();
        Task<IList<ItemResult>> GetGroceryItems();
        Task<ClientHasToken> GetClientHasTokenBy(ClientHasTokenFilter filter);
        Task InsertPaymentOrderAsync(PaymentOrderProcessRequest request);
        Task InsertPaymentTransactionWithTableTypeAsync(PaymentOrderProcessRequest request);
        Task InsertPaymentTrasactionStatusWithTableTypeAsync(PaymentOrderProcessRequest request);
        Task UpdatePaymentTransactionJsonRequestAsync(PaymentOrderProcessRequest request);
        Task<IList<GetTransactionByOrder>> GetTransactionbyOrder(string OrderID);
        Task<IList<GetJsonOrder>> GetJsonbyOrder(string PaymentTransactionID);
        Task<IList<CobroXMLResponse>> GetOrder(string OrderId);
        #endregion
    }
}
