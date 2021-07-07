using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.TokenManagement;
using Soriana.PPS.TokenManagement.CrearCliente.Constants;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.CrearCliente.Services
{
    public class CrearClienteService : ICrearClienteService
    {
        #region Private Fields
        private readonly ILogger<CrearClienteService> _Logger;
        private readonly CustomerAPI _CustomerAPI;
        #endregion
        #region Constructors
        public CrearClienteService(ICustomerAPI customerAPI,
                                    ILogger<CrearClienteService> logger)
        {
            _Logger = logger;
            _CustomerAPI = (customerAPI as CustomerAPI);
        }
        #endregion
        #region Public Methods
        public async Task<IActionResult> CrearCliente(PostCustomerRequest customerRequest)
        {
            if (customerRequest == null ||
                customerRequest.BuyerInformation == null ||
                customerRequest.ClientReferenceInformation == null ||
                customerRequest.MerchantDefinedInformation == null ||
                !customerRequest.MerchantDefinedInformation.Any())
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = CrearClienteConstants.CREAR_CLIENTE_SERVICE_NAME,
                    ContentRequest = customerRequest
                });
            var customersResponse = await _CustomerAPI.PostCustomerAsync(customerRequest);
            return new OkObjectResult(customersResponse);
        }
        #endregion
    }
}
