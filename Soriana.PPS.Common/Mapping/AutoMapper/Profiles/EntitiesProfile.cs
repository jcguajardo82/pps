using AutoMapper;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using System;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class EntitiesProfile : Profile
    {
        #region Constructors
        public EntitiesProfile()
        {
            CreateMap<string, DateTime>()
                .ConvertUsing<ToDateFromStringConverter>();
            CreateMap<string, TimeSpan>()
                .ConvertUsing<ToTimeFromStringConverter>();
            #region Mapping related to Payment Order Entities
            CreateMap<PaymentOrderProcessRequest, PaymentOrderJsonRequestTableType>()
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)))
                .ForMember(d => d.PaymentOrderJSONRequest, o => o.MapFrom(s => string.Empty));
            CreateMap<DTO.Salesforce.Item, PaymentOrderShipmentItemTableType>()
                .ForMember(d => d.ShippingItemCategory, o => o.MapFrom(s => s.ShippingItemCategory))
                .ForMember(d => d.ShippingItemEAN, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingItemEAN) ? 0 : Convert.ToInt32(s.ShippingItemEAN)))
                .ForMember(d => d.ShippingItemId, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingItemId) ? 0 : Convert.ToInt32(s.ShippingItemId)))
                .ForMember(d => d.ShippingItemName, o => o.MapFrom(s => s.ShippingItemName))
                .ForMember(d => d.ShippingItemPrice, o => o.MapFrom(s => s.ShippingItemPrice))
                .ForMember(d => d.ShippingItemQuantity, o => o.MapFrom(s => s.ShippingItemQuantity))
                .ForMember(d => d.ShippintItemTotal, o => o.MapFrom(s => s.ShippintItemTotal));
            CreateMap<PaymentOrderProcessRequest, PaymentOrderShipmentItemTableType>()
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)));
            CreateMap<Shipment, PaymentOrderShipmentTableType>()
                .ForMember(d => d.ShippingAddress, o => o.MapFrom(s => s.ShippingAddress))
                .ForMember(d => d.ShippingCity, o => o.MapFrom(s => s.ShippingCity))
                .ForMember(d => d.ShippingDeliveryDesc, o => o.MapFrom(s => s.ShippingDeliveryDesc))
                .ForMember(d => d.ShippingDeliveryId, o => o.MapFrom(s => s.ShippingDeliveryId))
                .ForMember(d => d.ShippingFirstName, o => o.MapFrom(s => s.ShippingFirstName))
                .ForMember(d => d.ShippingLastName, o => o.MapFrom(s => s.ShippingLastName))
                .ForMember(d => d.ShippingPaymentImport, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingPaymentImport) ? 0.0 : Convert.ToDouble(s.ShippingPaymentImport)))
                .ForMember(d => d.ShippingPaymentInstallments, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingPaymentInstallments) ? 0 : Convert.ToInt32(s.ShippingPaymentInstallments)))
                .ForMember(d => d.ShippingReferenceNumber, o => o.MapFrom(s => s.ShippingReferenceNumber))
                .ForMember(d => d.ShippingStoreId, o => o.MapFrom(s => s.ShippingStoreId))
                .ForMember(d => d.ShippingStoreName, o => o.MapFrom(s => s.ShippingStoreName));
            CreateMap<PaymentOrderProcessRequest, PaymentOrderShipmentTableType>()
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)));
            CreateMap<PaymentOrderProcessRequest, PaymentOrderTableType>()
                .ForMember(d => d.CustomerAddress, o => o.MapFrom(s => s.CustomerAddress))
                .ForMember(d => d.CustomerCity, o => o.MapFrom(s => s.CustomerCity))
                .ForMember(d => d.CustomerContact, o => o.MapFrom(s => s.CustomerContact))
                .ForMember(d => d.CustomerCountry, o => o.MapFrom(s => s.CustomerCountry))
                .ForMember(d => d.CustomerDeviceFingerPrintId, o => o.MapFrom(s => s.CustomerDeviceFingerPrintId))
                .ForMember(d => d.CustomerEmail, o => o.MapFrom(s => s.CustomerEmail))
                .ForMember(d => d.CustomerFirstName, o => o.MapFrom(s => s.CustomerFirstName))
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
                .ForMember(d => d.CustomerIPAddress, o => o.MapFrom(s => s.CustomerIPAddress))
                .ForMember(d => d.CustomerLastName, o => o.MapFrom(s => s.CustomerLastName))
                .ForMember(d => d.CustomerLoyaltyCardId, o => o.MapFrom(s => s.CustomerLoyaltyCardId))
                .ForMember(d => d.CustomerLoyaltyRedeemElectronicMoney, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemElectronicMoney) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemElectronicMoney)))
                .ForMember(d => d.CustomerLoyaltyRedeemMoney, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemMoney) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemMoney)))
                .ForMember(d => d.CustomerLoyaltyRedeemPoints, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemPoints) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemPoints)))
                .ForMember(d => d.CustomerPurchasesQuantity, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerPurchasesQuantity) ? 0 : Convert.ToInt32(s.CustomerPurchasesQuantity)))
                .ForMember(d => d.CustomerRegisteredDays, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerRegisteredDays) ? 0 : Convert.ToInt32(s.CustomerRegisteredDays)))
                .ForMember(d => d.CustomerState, o => o.MapFrom(s => s.CustomerState))
                .ForMember(d => d.CustomerZipCode, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerZipCode) ? 0 : Convert.ToInt32(s.CustomerZipCode)))
                .ForMember(d => d.OrderAmount, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderAmount) ? 0.0 : Convert.ToDouble(s.OrderAmount)))
                .ForMember(d => d.OrderCouponCode, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderCouponCode) ? string.Empty : s.OrderCouponCode))
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.OrderDateTime))
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)))
                .ForMember(d => d.OrderSaleChannel, o => o.MapFrom(s => s.OrderSaleChannel))
                .ForMember(d => d.OrderTime, o => o.MapFrom(s => s.OrderDateTime))
                .ForMember(d => d.PaymentCardCVV, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentCardCVV) ? 0 : Convert.ToInt16(s.PaymentCardCVV)))
                .ForMember(d => d.PaymentCardNIP, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentCardNIP) ? 0 : Convert.ToInt16(s.PaymentCardNIP)))
                .ForMember(d => d.PaymentProcessor, o => o.MapFrom(s => s.PaymentProcessor))
                .ForMember(d => d.PaymentSaveCard, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentSaveCard) ? false : Convert.ToBoolean(s.PaymentSaveCard)))
                .ForMember(d => d.PaymentToken, o => o.MapFrom(s => s.PaymentToken))
                .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType))
                .ForMember(d => d.ReturnURL, o => o.MapFrom(s => s.ReturnURL));
            #endregion
            #region Mapping related to Payment Order Entities
            CreateMap<PaymentOrderProcessRequest, PaymentTransactionJsonRequestTableType>()
                .ForMember(d => d.PaymentTransactionID, o => o.MapFrom(s => s.TransactionReferenceID))
                .ForMember(d => d.PaymentTransactionJSONRequest, o => o.MapFrom(s => string.Empty));
            CreateMap<DTO.Salesforce.Item, PaymentTransactionShipmentItemTableType>()
                .ForMember(d => d.ShippingItemCategory, o => o.MapFrom(s => s.ShippingItemCategory))
                .ForMember(d => d.ShippingItemEAN, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingItemEAN) ? 0 : Convert.ToInt32(s.ShippingItemEAN)))
                .ForMember(d => d.ShippingItemId, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingItemId) ? 0 : Convert.ToInt32(s.ShippingItemId)))
                .ForMember(d => d.ShippingItemName, o => o.MapFrom(s => s.ShippingItemName))
                .ForMember(d => d.ShippingItemPrice, o => o.MapFrom(s => s.ShippingItemPrice))
                .ForMember(d => d.ShippingItemQuantity, o => o.MapFrom(s => s.ShippingItemQuantity))
                .ForMember(d => d.ShippintItemTotal, o => o.MapFrom(s => s.ShippintItemTotal));
            CreateMap<PaymentOrderProcessRequest, PaymentTransactionShipmentItemTableType>()
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)))
                .ForMember(d => d.PaymentOrderID, o => o.MapFrom(s => s.PaymentOrderID))
                .ForMember(d => d.PaymentTransactionID, o => o.MapFrom(s => s.TransactionReferenceID));
            CreateMap<Shipment, PaymentTransactionShipmentTableType>()
                .ForMember(d => d.ShippingAddress, o => o.MapFrom(s => s.ShippingAddress))
                .ForMember(d => d.ShippingCity, o => o.MapFrom(s => s.ShippingCity))
                .ForMember(d => d.ShippingDeliveryDesc, o => o.MapFrom(s => s.ShippingDeliveryDesc))
                .ForMember(d => d.ShippingDeliveryId, o => o.MapFrom(s => s.ShippingDeliveryId))
                .ForMember(d => d.ShippingFirstName, o => o.MapFrom(s => s.ShippingFirstName))
                .ForMember(d => d.ShippingLastName, o => o.MapFrom(s => s.ShippingLastName))
                .ForMember(d => d.ShippingPaymentImport, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingPaymentImport) ? 0.0 : Convert.ToDouble(s.ShippingPaymentImport)))
                .ForMember(d => d.ShippingPaymentInstallments, o => o.MapFrom(s => string.IsNullOrEmpty(s.ShippingPaymentInstallments) ? 0 : Convert.ToInt32(s.ShippingPaymentInstallments)))
                .ForMember(d => d.ShippingReferenceNumber, o => o.MapFrom(s => s.ShippingReferenceNumber))
                .ForMember(d => d.ShippingStoreId, o => o.MapFrom(s => s.ShippingStoreId))
                .ForMember(d => d.ShippingStoreName, o => o.MapFrom(s => s.ShippingStoreName));
            CreateMap<PaymentOrderProcessRequest, PaymentTransactionShipmentTableType>()
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)))
                .ForMember(d => d.PaymentOrderID, o => o.MapFrom(s => s.PaymentOrderID))
                .ForMember(d => d.PaymentTransactionID, o => o.MapFrom(s => s.TransactionReferenceID));
            CreateMap<PaymentOrderProcessRequest, PaymentTransactionTableType>()
                .ForMember(d => d.CustomerAddress, o => o.MapFrom(s => s.CustomerAddress))
                .ForMember(d => d.CustomerCity, o => o.MapFrom(s => s.CustomerCity))
                .ForMember(d => d.CustomerContact, o => o.MapFrom(s => s.CustomerContact))
                .ForMember(d => d.CustomerCountry, o => o.MapFrom(s => s.CustomerCountry))
                .ForMember(d => d.CustomerDeviceFingerPrintId, o => o.MapFrom(s => s.CustomerDeviceFingerPrintId))
                .ForMember(d => d.CustomerEmail, o => o.MapFrom(s => s.CustomerEmail))
                .ForMember(d => d.CustomerFirstName, o => o.MapFrom(s => s.CustomerFirstName))
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
                .ForMember(d => d.CustomerIPAddress, o => o.MapFrom(s => s.CustomerIPAddress))
                .ForMember(d => d.CustomerLastName, o => o.MapFrom(s => s.CustomerLastName))
                .ForMember(d => d.CustomerLoyaltyCardId, o => o.MapFrom(s => s.CustomerLoyaltyCardId))
                .ForMember(d => d.CustomerLoyaltyRedeemElectronicMoney, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemElectronicMoney) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemElectronicMoney)))
                .ForMember(d => d.CustomerLoyaltyRedeemMoney, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemMoney) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemMoney)))
                .ForMember(d => d.CustomerLoyaltyRedeemPoints, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerLoyaltyRedeemPoints) ? 0.0 : Convert.ToDouble(s.CustomerLoyaltyRedeemPoints)))
                .ForMember(d => d.CustomerPurchasesQuantity, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerPurchasesQuantity) ? 0 : Convert.ToInt32(s.CustomerPurchasesQuantity)))
                .ForMember(d => d.CustomerRegisteredDays, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerRegisteredDays) ? 0 : Convert.ToInt32(s.CustomerRegisteredDays)))
                .ForMember(d => d.CustomerState, o => o.MapFrom(s => s.CustomerState))
                .ForMember(d => d.CustomerZipCode, o => o.MapFrom(s => string.IsNullOrEmpty(s.CustomerZipCode) ? 0 : Convert.ToInt32(s.CustomerZipCode)))
                .ForMember(d => d.OrderAmount, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderAmount) ? 0.0 : Convert.ToDouble(s.OrderAmount)))
                .ForMember(d => d.OrderCouponCode, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderCouponCode) ? string.Empty : s.OrderCouponCode))
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.OrderDateTime))
                .ForMember(d => d.OrderReferenceNumber, o => o.MapFrom(s => string.IsNullOrEmpty(s.OrderReferenceNumber) ? 0 : Convert.ToInt64(s.OrderReferenceNumber)))
                .ForMember(d => d.OrderSaleChannel, o => o.MapFrom(s => s.OrderSaleChannel))
                .ForMember(d => d.OrderTime, o => o.MapFrom(s => s.OrderDateTime))
                .ForMember(d => d.PaymentCardCVV, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentCardCVV) ? 0 : Convert.ToInt16(s.PaymentCardCVV)))
                .ForMember(d => d.PaymentCardNIP, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentCardNIP) ? 0 : Convert.ToInt16(s.PaymentCardNIP)))
                .ForMember(d => d.PaymentProcessor, o => o.MapFrom(s => s.PaymentProcessor))
                .ForMember(d => d.PaymentSaveCard, o => o.MapFrom(s => string.IsNullOrEmpty(s.PaymentSaveCard) ? false : Convert.ToBoolean(s.PaymentSaveCard)))
                .ForMember(d => d.PaymentToken, o => o.MapFrom(s => s.PaymentToken))
                .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType))
                .ForMember(d => d.ReturnURL, o => o.MapFrom(s => s.ReturnURL))
                .ForMember(d => d.PaymentTransactionID, o => o.MapFrom(s => s.TransactionReferenceID))
                .ForMember(d => d.MerchandiseType, o => o.MapFrom(s => Convert.ToInt32(s.MerchandiseType)))
                .ForMember(d => d.PaymentOrderID, o => o.MapFrom(s => Convert.ToInt32(s.PaymentOrderID)));
            CreateMap<PaymentOrderProcessRequest, PaymentTransactionStatusTableType>()
                .ForMember(d => d.AffiliationTypeID, o => o.MapFrom(s => Convert.ToByte(s.AffiliationType)))
                .ForMember(d => d.IsRetrying, o => o.MapFrom(s => s.IsRetrying))
                .ForMember(d => d.PaymentTransactionID, o => o.MapFrom(s => s.TransactionReferenceID));
            #endregion
        }
        #endregion
    }
}
