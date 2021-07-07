using Soriana.PPS.Common.DTO.Salesforce;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public interface ISetGenerateTokenService
    {
        Task SetGenerateTokenAsync(PaymentOrderProcessRequest request);
    }
}
