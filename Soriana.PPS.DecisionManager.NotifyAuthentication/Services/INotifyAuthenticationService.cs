using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.NotifyAuthentication.Services
{
    public interface INotifyAuthenticationService
    {
        #region Public Methods
        Task<NotifyAuthenticationResponse> NotifyAuthenticationAsync(NotifyAuthenticationRequest notifyAuthenticationRequest);
        #endregion
    }
}
