using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.Common.Compatibility.Services
{
    public interface INotifyAuthenticationSOAPService
    {
        #region Public Methods
        Task<string> NotifyAuthenticationAsync(dynamic notifyAuthenticationRequest);
        #endregion
    }
}
