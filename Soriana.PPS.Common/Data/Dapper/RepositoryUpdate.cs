using AutoMapper;
using Dapper;
using Soriana.PPS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data.Dapper
{
    public class RepositoryUpdate<T> : RepositoryBase, IRepositoryUpdate<T>
    {
        #region Constructors
        public RepositoryUpdate(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
        }
        #endregion
        #region Public Methods
        public virtual async Task<int> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new Exception(string.Format(DatabaseSchemaConstants.ERROR_MESSAGE_ENTITY_MUST_NOT_BE_NULL, typeof(T).Name));
            return await UnitOfWork.Connection.UpdateAsync(entity, UnitOfWork.Transaction);
        }

        public virtual async Task<IList<int>> UpdateListAsync(IList<T> entities)
        {
            IList<int> result = new List<int>();
            foreach (T entity in entities)
            {
                if (entity == null) continue;
                int id = await UnitOfWork.Connection.UpdateAsync(entity, UnitOfWork.Transaction);
                result.Add(id);
            }
            return result;
        }
        #endregion
    }
}
