using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data
{
    public interface IRepositoryCreate<T>
    {
        #region Public Methods
        Task<long?> InsertAsync(T entity);
        Task<IList<long?>> InsertListAsync(IList<T> listEntities);
        #endregion
    }
}
