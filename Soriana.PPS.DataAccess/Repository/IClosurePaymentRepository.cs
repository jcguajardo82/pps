using System.Threading.Tasks;
using System.Collections.Generic;

using Soriana.PPS.Common.DTO.ClosureOrder;

namespace Soriana.PPS.DataAccess.Repository
{
    public interface IClosurePaymentRepository
    {
        Task<long?> InsertAsync(ClosureOrderGroceyRequest entity);
        Task<IList<long?>> InsertListAsync(IList<ClosureOrderGroceyRequest> listEntities);
        Task<IList<GetTransactionByOrder>> GetTransactionbyOrder(string OrderID);
        Task<IList<GetJsonOrder>> GetJsonbyOrder(string PaymentTransactionID);
        Task<IList<CobroXMLResponse>> GetOrder(string OrderId);

    }
}
