using AutoMapper;
using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.TokenManagement.RetrieveCustomer.Constants;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.RetrieveCustomer.Services
{
    public class RetrieveCustomerService : ServiceBase, IRetrieveCustomerService
    {
        #region Private Fields
        private readonly ILogger<RetrieveCustomerService> _Logger;
        private readonly ICustomerAPI _CustomerAPI;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public RetrieveCustomerService(ICustomerAPI customerAPI,
                                        IMapper mapper,
                                        ILogger<RetrieveCustomerService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _CustomerAPI = customerAPI;
        }
        #endregion
        #region Public Methods
        public async Task<TmsCustomersResponse> RetrieveCustomer(SimpleCustomerRequest customerRequest)
        {
            ValidateRequest(customerRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.RetrieveCustomer.ToString()));
            TmsCustomersResponse customersResponse = _Mapper.Map<TmsCustomersResponse>(await _CustomerAPI.GetCustomerAsync(customerTokenId: customerRequest.CustomerTokenId, profileId: customerRequest.ProfileId));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.RetrieveCustomer.ToString()));
            return customersResponse;
        }

        protected override void ValidateRequest(object request)
        {
            SimpleCustomerRequest customerRequest = request as SimpleCustomerRequest;
            if (customerRequest == null || string.IsNullOrEmpty(customerRequest.CustomerTokenId))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = RetrieveCustomerConstants.RETRIEVE_CUSTOMER_SERVICE_NAME,
                    ContentRequest = customerRequest
                })
                {
                    ServiceInterface = ServicesEnum.RetrieveCustomer.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(RetrieveCustomerConstants.RETRIEVE_CUSTOMER_NO_VALIDATE_RESPONSE, ServicesEnum.DeleteCustomer.ToString()));
        }
        #endregion
    }
}
