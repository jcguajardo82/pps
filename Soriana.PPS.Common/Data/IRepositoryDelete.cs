using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data
{
    public interface IRepositoryDelete<T>
    {
        #region Public Methods
        Task DeleteAsync(T entity);
        Task DeleteListAsync(IList<T> entities);
        #endregion
    }
}
