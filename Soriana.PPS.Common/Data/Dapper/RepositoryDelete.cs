using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data.Dapper
{
    public class RepositoryDelete<T> : RepositoryBase, IRepositoryDelete<T>
    {
        #region Constructors
        public RepositoryDelete(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
        }
        #endregion
        #region Public Methods
        public virtual Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteListAsync(IList<T> entities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
