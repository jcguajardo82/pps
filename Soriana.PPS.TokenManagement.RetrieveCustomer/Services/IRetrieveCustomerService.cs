using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.RetrieveCustomer.Services
{
    public interface IRetrieveCustomerService
    {
        Task<TmsCustomersResponse> RetrieveCustomer(SimpleCustomerRequest customerRequest);
    }
}
