using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.TokenManagement;
using Soriana.PPS.TokenManagement.CrearCliente.Constants;
using Soriana.PPS.TokenManagement.CrearCliente.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.CrearCliente
{
    public class CrearClienteFunction
    {
        #region Private Fields
        private readonly ICrearClienteService _CrearClienteService;
        private readonly ILogger<CrearClienteFunction> _Logger;
        #endregion
        #region Constructors
        public CrearClienteFunction(ICrearClienteService crearClienteService,
                                    ILogger<CrearClienteFunction> logger)
        {
            _CrearClienteService = crearClienteService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(CrearClienteConstants.CREAR_CLIENTE_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.MESSAGE_EXECUTING, CrearClienteConstants.CREAR_CLIENTE_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    return new BadRequestObjectResult(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = CrearClienteConstants.CREAR_CLIENTE_NO_CONTENT_REQUEST, ContentRequest = null });
                request.Body.Position = 0;
                string jsonPostCustomerRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PostCustomerRequest customerRequest = JsonConvert.DeserializeObject<PostCustomerRequest>(jsonPostCustomerRequest);
                return await _CrearClienteService.CrearCliente(customerRequest);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, CrearClienteConstants.CREAR_CLIENTE_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.MINUS_CHAR, CharactersConstants.ESPACE_CHAR, CrearClienteConstants.CREAR_CLIENTE_FUNCTION_NAME),
                    DescriptionDetail = ex
                });
            }
        }
        #endregion
    }
}