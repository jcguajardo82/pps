using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.HttpClient;
using Soriana.PPS.Common.Services;
using Soriana.PPS.PaymentOrderProcess.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PaymentOrderProcess.Services
{
    public sealed class PaymentOrderProcessService : ServiceBase, IPaymentOrderProcessService
    {
        #region Private Fields
        private readonly ILogger<PaymentOrderProcessService> _Logger;
        private readonly ISplitPaymentOrderService _SplitPaymentOrderService;
        private readonly ISequencePaymentOrderTransactionService _SequencePaymentOrderTransactionService;
        private readonly IHttpClientService _HttpClientService;
        private readonly IGetMerchantDefinedDataService _MerchantDefinedDataService;
        private readonly ISavePaymentOrderService _SavePaymentOrderService;
        private readonly ICallCreateDecisionService _CallCreateDecisionService;
        private readonly ICallProcessPaymentService _CallProcessPaymentService;
        private readonly ICallCapturePaymentService _CallCapturePaymentService;
        private readonly ICallEnrollmentService _CallEnrollmentService;
        private readonly ICallValidateAuthenticationService _CallValidateAuthenticationService;
        private readonly ICallNotifyAuthenticationService _CallNotifyAuthenticationService;
        private readonly ISavePaymentTransactionStatusService _SavePaymentTransactionStatusService;
        private readonly ISavePaymentTransactionService _SavePaymentTransactionService;
        private readonly ISetGenerateTokenService _SetGenerateTokenService;
        private readonly ISetTransactionReferenceService _SetTransactionReferenceService;
        private readonly IUpdatePaymentTransactionJsonRequestService _UpdatePaymentTransactionJsonRequestService;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _Mapper;
        private readonly HttpClientListOptions _HttpClientListOptions;
        private readonly MerchantDefinedDataOptions _MerchantDefinedDataOptions;
        #endregion
        #region Constructors
        public PaymentOrderProcessService(IHttpClientService httpClientService,
                                            ISplitPaymentOrderService splitPaymentOrderService,
                                            ISequencePaymentOrderTransactionService sequencePaymentOrderTransactionService,
                                            IGetMerchantDefinedDataService merchantDefinedDataService,
                                            ISavePaymentOrderService savePaymentOrderService,
                                            ICallCreateDecisionService callCreateDecisionService,
                                            ICallProcessPaymentService callProcessPaymentService,
                                            ICallCapturePaymentService callCapturePaymentService,
                                            ICallEnrollmentService callEnrollmentService,
                                            ICallValidateAuthenticationService callValidateAuthenticationService,
                                            ICallNotifyAuthenticationService callNotifyAuthenticationService,
                                            ISavePaymentTransactionStatusService savePaymentTransactionStatusService,
                                            ISavePaymentTransactionService savePaymentTransactionService,
                                            ISetGenerateTokenService setGenerateTokenService,
                                            ISetTransactionReferenceService setTransactionReferenceService,
                                            IUpdatePaymentTransactionJsonRequestService updatePaymentTransactionJsonRequestService,
                                            IOptions<HttpClientListOptions> httpClientListOptions,
                                            IOptions<MerchantDefinedDataOptions> merchantDefinedDataOptions,
                                            IConfiguration configuration,
                                            IMapper mapper,
                                            ILogger<PaymentOrderProcessService> logger)
        {
            _HttpClientService = httpClientService;
            _SplitPaymentOrderService = splitPaymentOrderService;
            _SequencePaymentOrderTransactionService = sequencePaymentOrderTransactionService;
            _MerchantDefinedDataService = merchantDefinedDataService;
            _SavePaymentOrderService = savePaymentOrderService;
            _HttpClientListOptions = httpClientListOptions.Value;
            _MerchantDefinedDataOptions = merchantDefinedDataOptions.Value;
            _CallCreateDecisionService = callCreateDecisionService;
            _CallProcessPaymentService = callProcessPaymentService;
            _CallCapturePaymentService = callCapturePaymentService;
            _CallEnrollmentService = callEnrollmentService;
            _CallValidateAuthenticationService = callValidateAuthenticationService;
            _CallNotifyAuthenticationService = callNotifyAuthenticationService;
            _SavePaymentTransactionStatusService = savePaymentTransactionStatusService;
            _SavePaymentTransactionService = savePaymentTransactionService;
            _SetGenerateTokenService = setGenerateTokenService;
            _SetTransactionReferenceService = setTransactionReferenceService;
            _UpdatePaymentTransactionJsonRequestService = updatePaymentTransactionJsonRequestService;
            _Logger = logger;
            _Mapper = mapper;
            _Configuration = configuration;
        }
        #endregion
        #region Private Methods
        private bool IsValidation3DSRequired(string profileName, string transactionStatus)
        {
            if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(transactionStatus)) return false;
            if (transactionStatus == CreateDecisionStatus201Enum.PENDING_REVIEW.ToString() &&
                profileName.Contains(PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_VALIDATION_3DS))
                return true;
            return false;
        }

        private bool IsRetryRequired(string transactionStatus)
        {
            if (transactionStatus == PaymentStatus201Enum.DECLINED.ToString())
                return true;
            return false;
        }

        private PaymentOrderProcessResponse GetBadRequestResponse(PaymentOrderProcessRequest request, string responseErrorText)
        {
            return new PaymentOrderProcessResponse()
            {
                ResponseCode = ((int)HttpStatusCode.BadRequest).ToString(),
                ResponseError = HttpStatusCode.BadRequest.ToString(),
                ResponseErrorText = responseErrorText,
                ///
                /// TODO: PENDIENTE - Implementar mapeo PaymentOrderProcessRequest --> PaymentProcessTransaction 
                /// 
                Shipments = _Mapper.Map<IList<ShipmentResponse>>(request.Shipments)
            };
        }

        private PaymentOrderProcessResponse GetInternalServerErrorResponse(PaymentOrderProcessRequest request)
        {
            return new PaymentOrderProcessResponse()
            {
                ResponseCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                ResponseError = HttpStatusCode.InternalServerError.ToString(),
                ResponseErrorText = PaymentProcessorConstants.PAYMENT_FAILED_INTERNAL_ERROR,
                ///
                /// TODO: PENDIENTE - Implementar mapeo PaymentOrderProcessRequest --> PaymentProcessTransaction 
                /// 
                Shipments = _Mapper.Map<IList<ShipmentResponse>>(request.Shipments)
            };
        }
        #endregion
        #region Public Methods
        public async Task<PaymentOrderProcessResponse> PaymentOrderProcess(PaymentOrderProcessRequest paymentOrderProcessRequest)
        {
            string paymentOrderProcessRequestMessage = string.Empty;
            ////Step 1.- Validar request PaymentOrderProcessRequest
            ValidateRequest(paymentOrderProcessRequest);
            //Step 2.- Asignar numero de transaccion a Original PaymentOrderProcessRequest
            await _SetTransactionReferenceService.SetTransactionReferenceAsync(paymentOrderProcessRequest);
            //Step 3.- Guardar Original PaymentOrderProcessRequest
            await _SavePaymentOrderService.InsertPaymentOrderAsync(paymentOrderProcessRequest);
            // Step 4.- Dividir orden por Grocery y NonGrocery.
            _SplitPaymentOrderService.SplitPaymentOrderByMerchandiseType(paymentOrderProcessRequest, out PaymentOrderProcessRequest paymentOrderProcessRequestGrocery, out PaymentOrderProcessRequest paymentOrderProcessRequestNonGrocery);
            // Step 5.- Establecer secuencia de transaciones de ordenes
            IList<PaymentOrderProcessRequest> paymentOrderProcessRequestList = _SequencePaymentOrderTransactionService.GetSequencePaymentOrderTransaction(paymentOrderProcessRequestGrocery, paymentOrderProcessRequestNonGrocery);
            ///
            /// TODO: Step 6.- Como se aplica la logica del PaymentType y PaymentProcessor
            /// 
            if ((paymentOrderProcessRequest.PaymentType.ToLower() == PaymentThroughEnum.CARD.ToString().ToLower()
                && paymentOrderProcessRequest.PaymentProcessor.ToLower() == PaymentProcessorEnum.CYBER.ToString().ToLower()))
            {
                TransactionResponse transactionResponse;
                foreach (PaymentOrderProcessRequest paymentTransactionRequest in paymentOrderProcessRequestList)
                {
                    //Step 6.1.- Asignar numero de transaccion a transacciones o partidas de ordenes PaymentOrderProcessRequest
                    await _SetTransactionReferenceService.SetTransactionReferenceAsync(paymentTransactionRequest);
                    paymentOrderProcessRequestMessage = string.Format(PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_REQUEST_MESSAGE_SUCCESSFUL, paymentTransactionRequest.TransactionReferenceID);
                    //Step 6.2.- Generar un token del request
                    await _SetGenerateTokenService.SetGenerateTokenAsync(paymentTransactionRequest);
                    // Step 6.3.- Guardar transacciones o partidas de ordenes PaymentOrderProcessRequest 
                    await _SavePaymentTransactionService.InsertPaymentTransactionAsync(paymentTransactionRequest);
                    //Step 6.4.- CreateDecision
                    transactionResponse = await _CallCreateDecisionService.CreateDecisionAsync(paymentTransactionRequest, paymentOrderProcessRequest);
                    paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                    paymentTransactionRequest.PaymentTransactionService = ServicesEnum.CreateDecisionManager.ToString();
                    await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                    if (transactionResponse!.IsValid)
                    {
                        if (transactionResponse.Status == CreateDecisionStatus201Enum.PENDING_REVIEW.ToString())
                        {
                            string profileName = (transactionResponse.ResponseObject as DecisionManager201Response).Profile == null ? string.Empty : (transactionResponse.ResponseObject as DecisionManager201Response).Profile.Name;
                            if (IsValidation3DSRequired(profileName, transactionResponse.Status))
                            {
                                paymentTransactionRequest.Apply3DS = true;
                                /// 
                                /// TODO: Solicitar Inicialización
                                /// 
                                //Step 6.4.1.- Enrollment
                                transactionResponse = await _CallEnrollmentService.EnrollmentAsync(paymentTransactionRequest, paymentOrderProcessRequest);
                                paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                                paymentTransactionRequest.PaymentTransactionService = ServicesEnum.CheckPayerAuthEnrollment.ToString();
                                await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                                if (transactionResponse!.IsValid)
                                {
                                    if (transactionResponse.Status == CheckPayerAuthEnrollment201Enum.PENDING_AUTHENTICATION.ToString())
                                    {
                                        //Step 6.4.2.- ValidateAuthentication
                                        transactionResponse = await _CallValidateAuthenticationService.ValidateAuthenticationAsync(paymentTransactionRequest);
                                        paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                                        paymentTransactionRequest.PaymentTransactionService = ServicesEnum.ValidateAuthenticationResults.ToString();
                                        await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                                        if (transactionResponse!.IsValid)
                                        {
                                            paymentTransactionRequest.IsAuthenticated = true;
                                            paymentTransactionRequest.NotifyAuthenticationStatus = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_ACCEPT;
                                            //Step 6.5.- NotifyAuthentication
                                            transactionResponse = await _CallNotifyAuthenticationService.NotifyAuthenticationAsync(paymentTransactionRequest);
                                            paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                                            paymentTransactionRequest.PaymentTransactionService = ServicesEnum.NotifyAuthenticationDecisionManager.ToString();
                                            await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                                            if (!(transactionResponse!.IsValid))
                                                return GetInternalServerErrorResponse(paymentTransactionRequest);
                                        }
                                        else
                                        {
                                            paymentTransactionRequest.IsAuthenticated = false;
                                            if (transactionResponse.Status == ValidateAuthenticationResults201Enum.PENDING_AUTHENTICATION.ToString())
                                                return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_PENDING_AUTHENTICATION);
                                            if (transactionResponse.Status == ValidateAuthenticationResults201Enum.INVALID_REQUEST.ToString() ||
                                                transactionResponse.Status == ValidateAuthenticationResults201Enum.AUTHENTICATION_FAILED.ToString())
                                            {
                                                paymentTransactionRequest.NotifyAuthenticationStatus = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REJECT;
                                                return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_OTHER_PAYMENT_FORM);
                                            }
                                            else
                                                return GetInternalServerErrorResponse(paymentTransactionRequest);
                                        }
                                    }
                                }
                                else
                                {
                                    if (transactionResponse.Status == CheckPayerAuthEnrollment201Enum.AUTHENTICATION_FAILED.ToString() ||
                                        transactionResponse.Status == CheckPayerAuthEnrollment201Enum.INVALID_REQUEST.ToString() ||
                                        transactionResponse.Status == CheckPayerAuthEnrollment201Enum.PENDING_AUTHENTICATION.ToString())
                                        return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_OTHER_PAYMENT_FORM);
                                    else
                                        return GetInternalServerErrorResponse(paymentTransactionRequest);
                                }
                            }
                            else
                            {
                                return new PaymentOrderProcessResponse()
                                {
                                    ResponseCode = Convert.ToString((int)HttpStatusCode.Accepted),
                                    ResponseMessage = JsonConvert.SerializeObject(transactionResponse.ResponseObject as DecisionManager201Response)
                                };
                            }
                        }
                        else
                        {
                            paymentTransactionRequest.IsAuthenticated = true;
                            await _UpdatePaymentTransactionJsonRequestService.UpdatePaymentTransactionJsonRequestAsync(paymentTransactionRequest);
                        }
                    }
                    else
                    {
                        if (transactionResponse!.Status == CreateDecisionStatus201Enum.REJECTED.ToString() ||
                            transactionResponse!.Status == CreateDecisionStatus201Enum.DECLINED.ToString() ||
                            transactionResponse!.Status == CreateDecisionStatus201Enum.INVALID_REQUEST.ToString())
                            return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_OTHER_PAYMENT_FORM);
                        else
                            return GetInternalServerErrorResponse(paymentTransactionRequest);
                    }
                    paymentOrderProcessRequest.AffiliationType = AffiliationTypeEnum.Default;
                    paymentOrderProcessRequest.IsRetrying = false;
                    //Step 6.4.- ProcessPayment
                    do
                    {
                        transactionResponse = await _CallProcessPaymentService.ProcessPaymentAsync(paymentTransactionRequest, paymentOrderProcessRequest);
                        paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                        paymentTransactionRequest.PaymentTransactionService = ServicesEnum.ProcessPayment.ToString();
                        paymentOrderProcessRequest.TransactionAuthorizationId = transactionResponse.TransactionAuthorizationId;
                        await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                        await _UpdatePaymentTransactionJsonRequestService.UpdatePaymentTransactionJsonRequestAsync(paymentTransactionRequest);
                        if (transactionResponse!.IsValid)
                        {
                            paymentTransactionRequest.TransactionAuthorizationId = transactionResponse.TransactionAuthorizationId;
                            paymentTransactionRequest.IsAuthorized = true;
                            await _UpdatePaymentTransactionJsonRequestService.UpdatePaymentTransactionJsonRequestAsync(paymentTransactionRequest);
                        }
                        else
                        {
                            paymentTransactionRequest.IsAuthorized = false;
                            if (transactionResponse!.Status == PaymentStatus201Enum.PENDING_REVIEW.ToString() && !paymentTransactionRequest.IsRetrying)
                                return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_PROCESS_PAYMENT_PENDING_REVIEW);
                            else if (transactionResponse!.Status == PaymentStatus201Enum.PENDING_AUTHENTICATION.ToString() && !paymentTransactionRequest.IsRetrying)
                                return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_PROCESS_PAYMENT_PENDING_AUTHENTICATION);
                            else if (transactionResponse!.Status == PaymentStatus201Enum.INVALID_REQUEST.ToString() && !paymentTransactionRequest.IsRetrying)
                                return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_PROCESS_PAYMENT_INVALID_REQUEST);
                            else if ((transactionResponse!.Status == PaymentStatus201Enum.PARTIAL_AUTHORIZED.ToString() ||
                                    transactionResponse!.Status == PaymentStatus201Enum.AUTHORIZED_PENDING_REVIEW.ToString() ||
                                    transactionResponse!.Status == PaymentStatus201Enum.AUTHORIZED_RISK_DECLINED.ToString()) && !paymentTransactionRequest.IsRetrying)
                                return GetInternalServerErrorResponse(paymentTransactionRequest);
                            else if (IsRetryRequired(transactionResponse!.Status) && !paymentTransactionRequest.IsRetrying)
                            {
                                paymentTransactionRequest.IsRetrying = true;
                                if (paymentTransactionRequest.MerchandiseType == MerchandiseTypeEnum.GROCERY)
                                    paymentTransactionRequest.AffiliationType = AffiliationTypeEnum.EvoPayment;
                                else
                                    paymentTransactionRequest.AffiliationType = AffiliationTypeEnum.GetNet;
                            }
                            else
                            {
                                paymentTransactionRequest.IsRetrying = false;
                                if (transactionResponse!.Status == PaymentStatus201Enum.DECLINED.ToString())
                                    return GetBadRequestResponse(paymentTransactionRequest, PaymentProcessorConstants.PAYMENT_FAILED_OTHER_PAYMENT_FORM);
                                else
                                    return GetInternalServerErrorResponse(paymentTransactionRequest);
                            }
                        }
                    } while (paymentTransactionRequest.IsRetrying);
                    if ((!paymentTransactionRequest.IsAuthenticated || !paymentTransactionRequest.IsAuthorized))
                        break;
                }
                //Step 6.6.- CapturePayment
                if (paymentOrderProcessRequestList.Any(pop => !pop.IsAuthorized || !pop.IsAuthenticated))
                {
                    /// 
                    /// TODO: Caso para N transacciones
                    /// 
                    return GetBadRequestResponse(paymentOrderProcessRequest, PaymentProcessorConstants.PAYMENT_FAILED_OTHER_PAYMENT_FORM);
                }
                else
                {
                    foreach (PaymentOrderProcessRequest paymentTransactionRequest in paymentOrderProcessRequestList)
                    {
                        if (paymentTransactionRequest.MerchandiseType == MerchandiseTypeEnum.NONGROCERY)
                        {
                            transactionResponse = await _CallCapturePaymentService.CapturePaymentAsync(paymentTransactionRequest, paymentOrderProcessRequest);
                            paymentTransactionRequest.TransactionStatus = transactionResponse.Status;
                            paymentTransactionRequest.PaymentTransactionService = ServicesEnum.CapturePayment.ToString();
                            await _SavePaymentTransactionStatusService.InsertPaymentTransactionStatusAsync(paymentTransactionRequest);
                            if (transactionResponse!.IsValid)
                            {
                                ///
                                ///PENDIENTE: Escenario de Rollback
                                ///
                                return GetInternalServerErrorResponse(paymentTransactionRequest);
                            }
                        }
                    }
                }
                ///
                ///TODO: Implementar servicio DeleteSubcription para las subscripciones que no se guarden.
                ///
                //Step 6.6.- DeleteSubscription
                //DeleteSubscription(ClientId, Token);
            }
            ///
            /// TODO: Step 7.- PENDIENTE - Como se aplica la logica del PaymentType y PaymentProcessor
            /// 
            else if (paymentOrderProcessRequest.PaymentType.ToLower() == PaymentThroughEnum.INSTORE.ToString().ToLower())
            {
                ///
                /// TODO: Pendiente validar implementación para el caso CARD atraves de PAYPAL
                /// 
            }
            ///
            /// TODO: Step 8.- PENDIENTE - Como se aplica la logica del PaymentType y PaymentProcessor
            /// 
            else if (paymentOrderProcessRequest.PaymentType.ToLower() == PaymentThroughEnum.ONDELIVERY.ToString().ToLower())
            {
                ///
                /// TODO: Pendiente validar implementación para el caso CARD atraves de Programa de Lealtad
                /// 
            }
            ///
            /// TODO: Step 9.- PENDIENTE - Como se aplica la logica del PaymentType y PaymentProcessor
            /// 
            else if (paymentOrderProcessRequest.PaymentType.ToLower() == PaymentThroughEnum.WALLET.ToString().ToLower())
            {
                ///
                /// TODO: Pendiente validar implementación para el caso CARD atraves de Tarjeta de Puntos
                /// 
            }
            ///
            /// TODO: Step 10.- PENDIENTE - Como se aplica la logica del PaymentType y PaymentProcessor
            /// 
            else
            {
                ///
                /// TODO: PENDIENTE - validar implementación para el caso CASH
                /// 
            }
            return new PaymentOrderProcessResponse() { ResponseCode = ((int)HttpStatusCode.OK).ToString(), ResponseMessage = paymentOrderProcessRequestMessage };
        }
        #endregion
        #region Protected Methods
        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                paymentOrderProcessRequest.Shipments == null ||
                !paymentOrderProcessRequest.Shipments.Any() ||
                paymentOrderProcessRequest.Shipments.Any(i => i.Items == null || !i.Items.Any())
                )
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.PaymentOrderProcess.ToString()
                };
        }
        protected override void ValidateResponse(object response, object request = null)
        {
            PaymentOrderProcessResponse paymentOrderProcessResponse = response as PaymentOrderProcessResponse;
            ///
            ///TODO: Pendiente definicion de codigos de PaymentOrderProcessEnum
            ///
            if (!Enum.GetNames(typeof(PaymentOrderProcessEnum)).Any(pS => pS == paymentOrderProcessResponse.ResponseCode))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = paymentOrderProcessResponse
                })
                {
                    ServiceInterface = ServicesEnum.PaymentOrderProcess.ToString(),
                    ExecutedInnerService = ServicesEnum.PaymentOrderProcess.ToString()
                };
            }
        }
        #endregion
    }
}
