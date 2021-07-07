using AutoMapper;

namespace Soriana.PPS.Common.Data
{
    public class RepositoryCreation : RepositoryBase
    {
        #region Constructors
        public RepositoryCreation(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        { }
        #endregion
    }
}
