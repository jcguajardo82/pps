using Microsoft.AspNetCore.Mvc;
using Soriana.PPS.Common.DTO.TokenManagement;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.CrearCliente.Services
{
    public interface ICrearClienteService
    {
        Task<IActionResult> CrearCliente(PostCustomerRequest customerRequest);
    }
}
