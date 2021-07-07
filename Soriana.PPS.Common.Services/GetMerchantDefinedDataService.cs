using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters.Helpers;
using Soriana.PPS.Common.Services.Constants;
using Soriana.PPS.DataAccess.Filters;
using Soriana.PPS.DataAccess.PaymentProcess;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public sealed class GetMerchantDefinedDataService : ServiceBase, IGetMerchantDefinedDataService
    {
        #region Private Fields
        private readonly ILogger<GetMerchantDefinedDataService> _Logger;
        private readonly IPaymentProcessContext _PaymentProcessorContext;
        private readonly MerchantDefinedDataOptions _MerchantDefinedDataOptions;
        #endregion
        #region Constructors
        public GetMerchantDefinedDataService(IPaymentProcessContext paymentProcessContext,
                                            ILogger<GetMerchantDefinedDataService> logger,
                                            IOptions<MerchantDefinedDataOptions> options)
        {
            _PaymentProcessorContext = paymentProcessContext;
            _Logger = logger;
            ValidateOptions(options);
            _MerchantDefinedDataOptions = options.Value;
        }
        #endregion
        #region Private Methods
        private void ValidateOptions(IOptions<MerchantDefinedDataOptions> options)
        {
            if (options == null || options.Value == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_OPTIONS),
                    ContentRequest = options
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
        }
        private async Task SetCustomMerchantDefinedData(PaymentOrderProcessRequest paymentOrderProcessRequest)
        {
            ClientHasTokenFilter filter = new ClientHasTokenFilter();
            filter.CustomerID = paymentOrderProcessRequest.CustomerId;
            filter.ClientToken = paymentOrderProcessRequest.PaymentToken.Split(CharactersConstants.HYPHEN_CHAR)[1];
            filter.IsActive = true;
            ClientHasToken clientHasToken = await _PaymentProcessorContext.GetClientHasTokenBy(filter);
            if (clientHasToken == null ||
                clientHasToken.ClientID == 0 ||
                string.IsNullOrEmpty(clientHasToken.Bank) ||
                clientHasToken.BinCode == 0 ||
                string.IsNullOrEmpty(clientHasToken.ClientToken) ||
                string.IsNullOrEmpty(clientHasToken.PaymentMethod) ||
                string.IsNullOrEmpty(clientHasToken.TypeOfCard) ||
                !clientHasToken.IsActive
                )
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_CLIENT_HAS_TOKEN),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            MerchantDefinedData binCode = paymentOrderProcessRequest.MerchantDefinedData.Where(mdd => mdd.Value == PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BIN_FIELD).FirstOrDefault();
            if (binCode == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_BIN),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            binCode.Value = clientHasToken.BinCode.ToString();
            MerchantDefinedData paymentMethod = paymentOrderProcessRequest.MerchantDefinedData.Where(mdd => mdd.Value == PaymentProcessorConstants.MERCHANT_DEFINED_DATA_PAYMENT_METHOD_FIELD).FirstOrDefault();
            if (paymentMethod == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_PAYMENT_METHOD),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            paymentMethod.Value = clientHasToken.PaymentMethod;
            MerchantDefinedData bank = paymentOrderProcessRequest.MerchantDefinedData.Where(mdd => mdd.Value == PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BANK_FIELD).FirstOrDefault();
            if (bank == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_BANK),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            bank.Value = clientHasToken.Bank;
            MerchantDefinedData typeOfCard = paymentOrderProcessRequest.MerchantDefinedData.Where(mdd => mdd.Value == PaymentProcessorConstants.MERCHANT_DEFINED_DATA_TYPE_OF_CARD_FIELD).FirstOrDefault();
            if (typeOfCard == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        CharactersConstants.HYPHEN_CHAR,
                                                        CharactersConstants.ESPACE_CHAR,
                                                        ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_TYPE_OF_CARD),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            typeOfCard.Value = clientHasToken.TypeOfCard;
        }
        private void ValidateMerchantDefinedData(PaymentOrderProcessRequest paymentOrderProcessRequest)
        {
            byte merchandiseType = PaymentOrderProcessRequestHelper.GetMerchandiseType(paymentOrderProcessRequest, ServicesEnum.MerchantDefinedData.ToString());
            switch (merchandiseType)
            {
                case (byte)MerchandiseTypeEnum.GROCERY:
                    if (paymentOrderProcessRequest.MerchantDefinedData.Count != _MerchantDefinedDataOptions.GroceryConfigurationNumber)
                        throw new BusinessException(new BusinessResponse()
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Description = HttpStatusCode.BadRequest.ToString(),
                            DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                                CharactersConstants.ESPACE_CHAR,
                                                                CharactersConstants.HYPHEN_CHAR,
                                                                CharactersConstants.ESPACE_CHAR,
                                                                ServicesCommonConstants.CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_GROCERY_CONFIGURATION_NUMBER),
                            ContentRequest = paymentOrderProcessRequest
                        })
                        {
                            ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                        };
                    break;
                case (byte)MerchandiseTypeEnum.NONGROCERY:
                    if (paymentOrderProcessRequest.MerchantDefinedData.Count != _MerchantDefinedDataOptions.NonGroceryConfigurationNumber)
                        throw new BusinessException(new BusinessResponse()
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Description = HttpStatusCode.BadRequest.ToString(),
                            DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                                CharactersConstants.ESPACE_CHAR,
                                                                CharactersConstants.HYPHEN_CHAR,
                                                                CharactersConstants.ESPACE_CHAR,
                                                                ServicesCommonConstants.CONFIGURATION_BUG_MERCHANT_DEFINED_DATA_NON_GROCERY_CONFIGURATION_NUMBER),
                            ContentRequest = paymentOrderProcessRequest
                        })
                        {
                            ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                        };
                    break;
                default:
                    throw new BusinessException(new BusinessResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString(),
                        DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                            CharactersConstants.ESPACE_CHAR,
                                                            CharactersConstants.HYPHEN_CHAR,
                                                            CharactersConstants.ESPACE_CHAR,
                                                            ServicesCommonConstants.CONFIGURATION_BUG_MERCHANDISE_TYPE),
                        ContentRequest = paymentOrderProcessRequest
                    })
                    {
                        ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                    };
            }
        }
        #endregion
        #region Public Methods
        public async Task<IList<MerchantDefinedData>> GetMerchantDefinedData(PaymentOrderProcessRequest paymentOrderProcessRequest, PaymentOrderProcessRequest originalPaymentOrderProcessRequest, string serviceInterface)
        {
            ValidateRequest(paymentOrderProcessRequest);
            paymentOrderProcessRequest.MerchantDefinedData = PaymentOrderProcessRequestHelper.GetMerchantDefinedData(paymentOrderProcessRequest, originalPaymentOrderProcessRequest, serviceInterface);
            ValidateMerchantDefinedData(paymentOrderProcessRequest);
            if (paymentOrderProcessRequest.MerchandiseType == MerchandiseTypeEnum.GROCERY)
                await SetCustomMerchantDefinedData(paymentOrderProcessRequest);
            else if (paymentOrderProcessRequest.MerchandiseType == MerchandiseTypeEnum.NONGROCERY)
                await SetCustomMerchantDefinedData(paymentOrderProcessRequest);
            else
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                                                            CharactersConstants.ESPACE_CHAR,
                                                            CharactersConstants.HYPHEN_CHAR,
                                                            CharactersConstants.ESPACE_CHAR,
                                                            ServicesCommonConstants.CONFIGURATION_BUG_MERCHANDISE_TYPE),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
            ValidateResponse(paymentOrderProcessRequest.MerchantDefinedData, paymentOrderProcessRequest);
            return paymentOrderProcessRequest.MerchantDefinedData;
        }

        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                string.IsNullOrEmpty(paymentOrderProcessRequest.CustomerId) ||
                string.IsNullOrEmpty(paymentOrderProcessRequest.PaymentToken))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            IList<MerchantDefinedData> merchantDefinedDataResponse = response as IList<MerchantDefinedData>;
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            string message = string.Empty;
            if (paymentOrderProcessRequest.MerchandiseType == MerchandiseTypeEnum.GROCERY && paymentOrderProcessRequest.MerchantDefinedData.Count != _MerchantDefinedDataOptions.GroceryConfigurationNumber)
                message = ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_GROCERY_CONFIGURATION_NUMBER;
            else if (paymentOrderProcessRequest.MerchandiseType == MerchandiseTypeEnum.NONGROCERY && paymentOrderProcessRequest.MerchantDefinedData.Count != _MerchantDefinedDataOptions.NonGroceryConfigurationNumber)
                message = ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_INVALID_NON_GROCERY_CONFIGURATION_NUMBER;
            if (!string.IsNullOrEmpty(message))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.MERCHANT_DEFINED_DATA_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, message),
                    ContentRequest = request,
                    ContentResponse = response
                })
                {
                    ServiceInterface = ServicesEnum.MerchantDefinedData.ToString(),
                    ExecutedInnerService = ServicesEnum.MerchantDefinedData.ToString()
                };
        }
        #endregion
    }
}
