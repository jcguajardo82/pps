using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.DataAccess.Filters;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public interface IClientHasTokenRepository : IRepositoryCreate<ClientHasToken>, IRepositoryRead<ClientHasToken>
    {
        #region Public Methods
        Task<ClientHasToken> GetClientHasTokenBy(ClientHasTokenFilter filter);
        #endregion
    }
}
