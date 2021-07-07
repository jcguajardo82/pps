using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data
{
    public interface IRepositoryUpdate<T>
    {
        #region Public Methods
        Task<int> UpdateAsync(T entity);
        Task<IList<int>> UpdateListAsync(IList<T> entities);
        #endregion
    }
}
