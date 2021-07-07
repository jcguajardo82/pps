using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class DecisionManagerProfile : Profile
    {
        #region Constructor
        public DecisionManagerProfile()
        {
            //DecisionClientReferenceInformation
            CreateMap<string, Riskv1decisionsClientReferenceInformation>()
                .ConvertUsing<DecisionManagerClientReferenceTypeConverter>();
            //DecisionPaymentInformacion
            CreateMap<string, Riskv1decisionsPaymentInformation>()
                .ConvertUsing<DecisionManagerPaymentInformationTypeConverter>();
            //DecisionOrderInformacion
            CreateMap<PaymentOrderProcessRequest, Riskv1decisionsOrderInformation>()
                .ConvertUsing<DecisionManagerOrderInformationTypeConverter>();
            //DecisionOrderInformacionLineItems
            CreateMap<Item, RiskDecisionsOrderInformationLineItems>()
                ///TODO: Pendiente validar cual es el valor correcto del ProductCode entre Salesforce y Cybersource
                .ForMember(d => d.ProductCode, s => s.MapFrom(o => o.ShippingItemCategory))
                .ForMember(d => d.ProductName, s => s.MapFrom(o => o.ShippingItemName))
                ///TODO: Pendiente validar cual es el valor correcto del SKU entre Salesforce y Cybersource
                .ForMember(d => d.ProductSKU, s => s.MapFrom(o => o.ShippingItemEAN))
                .ForMember(d => d.Quantity, s => s.MapFrom(o => o.ShippingItemQuantity))
                ///TODO: Pendiente validar cual es el valor correcto del Price entre Salesforce y Cybersource
                .ForMember(d => d.UnitPrice, s => s.MapFrom(o => o.ShippingItemPrice))
                ///TODO: Pendiente validar cual es el valor correcto del SKU entre Salesforce y Cybersource
                .ForMember(d => d.DistributorProductSku, s => s.MapFrom(o => o.ShippingItemEAN))
                ///TODO: Pendiente validar cual es el valor correcto del ProductDescription entre Salesforce y Cybersource
                .ForMember(d => d.ProductDescription, s => s.MapFrom(o => o.ShippingItemCategory));
            ///TODO: Pendiente validar campo: Tax Amount
            ///.ForMember(d=> d.TaxAmount, s=> s.MapFrom(o=> o.));
            //DecisionDeviceInformation
            CreateMap<string, Riskv1decisionsDeviceInformation>()
                .ConvertUsing<DecisionManagerDeviceInformationTypeConverter>();
            //DecisionMerchantDefinedData
            CreateMap<MerchantDefinedData, Riskv1decisionsMerchantDefinedInformation>();
            //DecisionMerchantDefinedInformation
            //CreateMap<PaymentOrderProcessRequest, List<RiskDecisionsMerchantDefinedInformation>>()
            //    .ConvertUsing<DecisionManagerMerchantDefinedInformationTypeConverter>();
            //DecisionRequest
            CreateMap<PaymentOrderProcessRequest, DecisionManagerRequest>()
                ///TODO: Pendiente validar si OrderReferenceNumber equivale a ClientReferenceInformation
                .ForMember(d => d.ClientReferenceInformation, s => s.MapFrom(o => o.OrderReferenceNumber))
                ///TODO: Pendiente validar si CustomerId equivale a RiskDecisionsPaymentInformation.PtsPaymentsPaymentInformationCustomer.CustomerId
                .ForMember(d => d.PaymentInformation, s => s.MapFrom(o => o.PaymentToken))
                ///TODO: Pendiente validar OrderInformation case Currency MXN
                .ForMember(d => d.OrderInformation, s => s.MapFrom(o => o))
                .ForMember(d => d.DeviceInformation, s => s.MapFrom(o => o.CustomerDeviceFingerPrintId))
                ///TODO: Pendiente validar la información MDD
                .ForMember(d => d.MerchantDefinedInformation, s => s.MapFrom(o => (o.MerchantDefinedData == null) ? new List<MerchantDefinedData>() : o.MerchantDefinedData.ToList()));
        }
        #endregion
    }
}
