using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class ValidateAuthenticationProfile : Profile
    {
        #region Constructors
        public ValidateAuthenticationProfile()
        {
            //ValidateAuthenticationClientReferenceInformation
            CreateMap<string, Riskv1authenticationsetupsClientReferenceInformation>()
                .ConvertUsing<ValidateAuthenticationRiskAuthenticationSetupsClientReferenceInformationTypeConverter>();
            //ValidateAuthenticationPaymentInformacion
            CreateMap<PaymentOrderProcessRequest, Riskv1authenticationresultsPaymentInformation>()
                .ConvertUsing<ValidateAuthenticationPaymentInformationTypeConverter>();
            //ValidateAuthenticationOrderInformacion
            CreateMap<PaymentOrderProcessRequest, Riskv1authenticationresultsOrderInformation>()
                .ConvertUsing<ValidateAuthenticationOrderInformationTypeConverter>();
            //ValidateAuthenticationOrderInformacionLineItems
            CreateMap<Item, RiskAuthenticationResultsOrderInformationLineItems>()
                .ForMember(d => d.Quantity, s => s.MapFrom(o => o.ShippingItemQuantity))
                ///TODO: Pendiente validar cual es el valor correcto del Price entre Salesforce y Cybersource
                .ForMember(d => d.UnitPrice, s => s.MapFrom(o => o.ShippingItemPrice));
            ///TODO: Pendiente validar campo: Tax Amount
            ///.ForMember(d=> d.TaxAmount, s=> s.MapFrom(o=> o.));
            //ValidateAuthenticationConsumerAuthenticationInformation
            CreateMap<PaymentOrderProcessRequest, Riskv1authenticationresultsConsumerAuthenticationInformation>()
                .ConvertUsing<ValidateAuthenticationRiskAuthenticationResultsConsumerAuthenticationInformationTypeConverter>();
            //ValidateAuthenticationRequest
            CreateMap<PaymentOrderProcessRequest, ValidateAuthenticationRequest>()
                ///TODO: Pendiente validar si OrderReferenceNumber equivale a ClientReferenceInformation
                .ForMember(d => d.ClientReferenceInformation, s => s.MapFrom(o => o.OrderReferenceNumber))
                ///TODO: Pendiente validar si CustomerId equivale a RiskDecisionsPaymentInformation.PtsPaymentsPaymentInformationCustomer.CustomerId
                .ForMember(d => d.PaymentInformation, s => s.MapFrom(o => o))
                ///TODO: Pendiente validar OrderInformation case Currency MXN
                .ForMember(d => d.OrderInformation, s => s.MapFrom(o => o))
                //ValidateAuthenticationConsumerAuthenticationInformation
                .ForMember(d => d.ConsumerAuthenticationInformation, s => s.MapFrom(o => o));
        }
        #endregion
    }
}
