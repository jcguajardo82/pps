using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.DeleteCustomer.Services
{
    public interface IDeleteCustomerService
    {
        Task DeleteCustomer(SimpleCustomerRequest customerRequest);
    }
}
