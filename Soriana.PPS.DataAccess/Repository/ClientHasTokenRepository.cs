using AutoMapper;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.DataAccess.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class ClientHasTokenRepository : RepositoryBase, IClientHasTokenRepository
    {
        #region Private Fields
        private readonly IRepositoryRead<ClientHasToken> _RepositoryRead;
        private readonly IRepositoryCreate<ClientHasToken> _RepositoryCreate;
        #endregion
        #region Constructors
        public ClientHasTokenRepository(IUnitOfWork unitOfWork,
                                        IRepositoryCreate<ClientHasToken> repositoryCreate,
                                        IRepositoryRead<ClientHasToken> repositoryRead, IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryRead = repositoryRead;
            _RepositoryCreate = repositoryCreate;
        }
        #endregion
        #region Public Methods
        #region RepositoryRead
        public async Task<ClientHasToken> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<ClientHasToken>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<ClientHasToken>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }

        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }

        public async Task<ClientHasToken> GetClientHasTokenBy(ClientHasTokenFilter filter)
        {
            ClearCommand();
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_GET_CLIENT_HAS_TOKEN_BY);
            SetParameters(new { CustomerID = filter.CustomerID, ClientToken = filter.ClientToken, IsActive = filter.IsActive });
            IList<ClientHasToken> clientHasTokens = await ExecuteProcedureWithSingleResult<ClientHasToken>(Parameters);
            ClearParameters();
            ClearCommand();
            return clientHasTokens.FirstOrDefault();
        }
        #endregion
        #region RepositoryWrite
        public async Task<long?> InsertAsync(ClientHasToken entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<ClientHasToken> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }
        #endregion
        #endregion
    }
}
