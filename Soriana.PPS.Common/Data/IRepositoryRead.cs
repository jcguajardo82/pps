using Soriana.PPS.Common.DTO.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data
{
    public interface IRepositoryRead<T>
    {
        #region Public Methods
        Task<T> GetByAsync(ISearchFilter searchFilter);
        Task<IList<T>> GetListAsync(ISearchFilter searchFilter);
        Task<IList<T>> GetListPagedAsync(ISearchFilter searchFilter);
        Task<int> RecordCountAsync(ISearchFilter searchFilter);
        #endregion
    }
}
