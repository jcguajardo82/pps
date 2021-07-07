using AutoMapper;
using Dapper;
using Soriana.PPS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data.Dapper
{
    public class RepositoryWrite<T> : RepositoryBase, IRepositoryCreate<T>
    {
        #region Constructors
        public RepositoryWrite(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
        }
        #endregion
        #region Public Methods
        public virtual async Task<long?> InsertAsync(T entity)
        {
            if (entity == null)
                throw new Exception(string.Format(DatabaseSchemaConstants.ERROR_MESSAGE_ENTITY_MUST_NOT_BE_NULL, typeof(T).Name));
            return await UnitOfWork.Connection.InsertAsync(entity, UnitOfWork.Transaction);
        }

        public virtual async Task<IList<long?>> InsertListAsync(IList<T> listEntities)
        {
            IList<long?> result = new List<long?>();
            foreach (T entity in listEntities)
            {
                if (entity == null) continue;
                long? id = await UnitOfWork.Connection.InsertAsync(entity, UnitOfWork.Transaction);
                result.Add(id);
            }
            return result;
        }
        #endregion
    }
}
