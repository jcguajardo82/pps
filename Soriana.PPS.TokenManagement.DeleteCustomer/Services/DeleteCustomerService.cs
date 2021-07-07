using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.TokenManagement.DeleteCustomer.Constants;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.DeleteCustomer.Services
{
    public class DeleteCustomerService : ServiceBase, IDeleteCustomerService
    {
        #region Private Fields
        private readonly ILogger<DeleteCustomerService> _Logger;
        private readonly ICustomerAPI _CustomerAPI;
        #endregion
        #region Constructors
        public DeleteCustomerService(ICustomerAPI customerAPI, ILogger<DeleteCustomerService> logger)
        {
            _Logger = logger;
            _CustomerAPI = customerAPI;
        }
        #endregion
        #region Public Methods
        public async Task DeleteCustomer(SimpleCustomerRequest customerRequest)
        {
            ValidateRequest(customerRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.DeleteCustomer.ToString()));
            await _CustomerAPI.DeleteCustomerAsync(customerTokenId: customerRequest.CustomerTokenId, profileId: customerRequest.ProfileId);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.DeleteCustomer.ToString()));
        }

        protected override void ValidateRequest(object request)
        {
            SimpleCustomerRequest customerRequest = request as SimpleCustomerRequest;
            if (customerRequest == null || string.IsNullOrEmpty(customerRequest.CustomerTokenId))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = DeleteCustomerConstants.DELETE_CUSTOMER_SERVICE_NAME,
                    ContentRequest = customerRequest
                })
                {
                    ServiceInterface = ServicesEnum.DeleteCustomer.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(DeleteCustomerConstants.DELETE_CUSTOMER_NO_VALIDATE_RESPONSE, ServicesEnum.DeleteCustomer.ToString()));
        }
        #endregion
    }
}
