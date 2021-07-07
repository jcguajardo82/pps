using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.DataAccess.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public interface IPaymentTransactionRepository : IRepositoryCreate<PaymentTransaction>, IRepositoryRead<PaymentTransaction>
    {
        #region Public Methods
        Task<long> GetSequenceAsync();

        Task<IList<PaymentTransaction>> GetChildrenPaymentTransactionAsync(PaymentProcessTransactionFilter filter);

        Task<PaymentTransaction> GetPaymentTransactionAsync(PaymentProcessTransactionFilter filter);

        Task InsertPaymentTransactionWithTableTypeAsync(PaymentOrderProcessRequest request);
        #endregion
    }
}
