using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public interface IItemRepository : IRepositoryCreate<Item>, IRepositoryRead<Item>
    {
        #region Public Methods
        Task<IList<ItemResult>> GetGroceryItems();

        Task<IList<ItemResult>> GetNonGroceryItems();
        #endregion
    }
}
