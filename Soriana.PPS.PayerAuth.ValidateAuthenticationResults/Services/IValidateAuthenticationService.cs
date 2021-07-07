using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.ValidateAuthentication.Services
{
    public interface IValidateAuthenticationService
    {
        Task<ValidateAuthentication201Response> ValidateAuthentication(ValidateAuthenticationRequest validateRequest);
    }
}
