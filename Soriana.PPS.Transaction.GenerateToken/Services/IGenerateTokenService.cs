using Soriana.PPS.Common.DTO.Common;
using System.Threading.Tasks;

namespace Soriana.PPS.Security.GenerateToken.Services
{
    public interface IGenerateTokenService
    {
        #region Public Methods
        Task<JsonWebTokenResponse> GenerateToken(JsonWebTokenRequest jwtRequest);
        #endregion
    }
}
