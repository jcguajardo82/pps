using AutoMapper;
using Dapper;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data.Dapper
{
    public class RepositoryRead<T> : RepositoryBase, IRepositoryRead<T>
    {
        #region Constructors
        public RepositoryRead(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
        }
        #endregion
        #region Public Methods
        public virtual async Task<T> GetByAsync(ISearchFilter searchFilter)
        {
            if (searchFilter == null)
                throw new Exception(DatabaseSchemaConstants.ERROR_MESSAGE_SEARCH_FILTER);
            return await UnitOfWork.Connection.GetAsync<T>(searchFilter.Parameters, UnitOfWork.Transaction);
        }

        public virtual async Task<IList<T>> GetListAsync(ISearchFilter searchFilter = null)
        {
            IList<T> list = new List<T>();
            if (searchFilter == null)
                foreach (T item in await UnitOfWork.Connection.GetListAsync<T>()) list.Add(item);
            else
                foreach (T item in await UnitOfWork.Connection.GetListAsync<T>(searchFilter.ConditionClause, searchFilter.Parameters, UnitOfWork.Transaction)) list.Add(item);
            return list;
        }

        public virtual async Task<IList<T>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            if (searchFilter == null)
                throw new Exception(DatabaseSchemaConstants.ERROR_MESSAGE_SEARCH_FILTER);
            IList<T> list = new List<T>();
            foreach (T item in await UnitOfWork.Connection.GetListPagedAsync<T>(searchFilter.PageNumber, searchFilter.RowsNumber, searchFilter.ConditionClause, searchFilter.OrderClause, searchFilter.Parameters, UnitOfWork.Transaction))
                list.Add(item);
            return list;
        }

        public virtual async Task<int> RecordCountAsync(ISearchFilter searchFilter = null)
        {
            return (searchFilter == null) ? await UnitOfWork.Connection.RecordCountAsync<int>(transaction: UnitOfWork.Transaction) : await UnitOfWork.Connection.RecordCountAsync<T>(searchFilter.ConditionClause, searchFilter.Parameters, UnitOfWork.Transaction);
        }
        #endregion
    }
}













