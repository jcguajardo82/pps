using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters.Helpers
{
    public static class PaymentOrderProcessRequestHelper
    {
        #region Private Methods
        private static void IsPaymentOrderProcessRequestValid(PaymentOrderProcessRequest paymentOrderProcessRequest, string serviceInterface)
        {
            if (paymentOrderProcessRequest == null)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ConvertersConstants.PAYMENT_ORDER_PROCESS_REQUEST,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = serviceInterface
                };
        }
        private static IList<MerchantDefinedData> GetMerchantDefinedDataGrocery(PaymentOrderProcessRequest paymentOrderProcessRequest, bool hasOriginalRequestOnlyMerchandiseType)
        {
            IList<MerchantDefinedData> merchantDefinedDataList = new List<MerchantDefinedData>();
            MerchantDefinedData merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "1";
            //Payment Method - Posible values: VISA, MASTERCARD, AMEX, CARNET, PRIVADA
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_PAYMENT_METHOD_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            ///
            /// TODO: Pendiente validar como trabaja los meses intereses, si es por shipments en particular o en general.
            ///
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "2";
            //Number of Installments - Posible values: 0, 3, 6, 12, 18
            merchantDefinedData.Value = paymentOrderProcessRequest.Shipments.FirstOrDefault() == null ? "0" : paymentOrderProcessRequest.Shipments.FirstOrDefault().ShippingPaymentInstallments;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "3";
            //Sale Channel - Posible values: WEB, APP
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderSaleChannel;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "4";
            //Num of days buyer is registered - Posible values: Empty
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerRegisteredDays;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "5";
            //Guest Buyer (Y/N) - Posible values: false
            merchantDefinedData.Value = "false";
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "6";
            //Number of purchase for customer - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerPurchasesQuantity;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "7";
            //Customer Contact Number - Posible values: 10 digits
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerContact;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "8";
            //Shipping DeadLine (Num Dias) - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "9";
            //Delivery Method - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.Shipments.Select(s => s.ShippingDeliveryDesc).FromEnumerableStringToString();
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "10";
            //Customer Loyality Number - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "11";
            //Promotional / Coupon Code - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderCouponCode;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "12";
            //Order Number - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderReferenceNumber;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "13";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Bin - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BIN_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "14";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Bank - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BANK_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "15";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Type of Card - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_TYPE_OF_CARD_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "16";
            //Loyalty Amount - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "17";
            //Wallets - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "18";
            //Second email - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "19";
            //Related consigment - Posible values: N/A
            merchantDefinedData.Value = (hasOriginalRequestOnlyMerchandiseType) ? PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD : paymentOrderProcessRequest.OrderReferenceNumber;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "20";
            //Order Status - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //Shipment
            Shipment shipment = paymentOrderProcessRequest.Shipments.FirstOrDefault();
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "21";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Store Id - Posible values: variant
            else
                merchantDefinedData.Value = shipment.ShippingStoreId; //Store Id - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "22";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Store Name - Posible values: variant
            else
                merchantDefinedData.Value = shipment.ShippingStoreName; //Store Name - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "23";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Store City - Posible values: variant
            else
                merchantDefinedData.Value = shipment.ShippingCity; //Store City - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "24";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Store State - Posible values: variant
            else
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Store State - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "25";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Store CP - Posible values: variant
            else
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Store CP - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "26";
            if (shipment == null)
                merchantDefinedData.Value = string.Empty; //Supplier Name - Posible values: variant
            else
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Supplier Name - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            return merchantDefinedDataList;
        }
        private static IList<MerchantDefinedData> GetMerchantDefinedDataNonGrocery(PaymentOrderProcessRequest paymentOrderProcessRequest, bool hasOriginalRequestOnlyMerchandiseType)
        {
            IList<MerchantDefinedData> merchantDefinedDataList = new List<MerchantDefinedData>();
            MerchantDefinedData merchantDefinedData = new MerchantDefinedData();
            //
            merchantDefinedData.Key = "1";
            //Payment Method - Posible values: VISA, MASTERCARD, AMEX, CARNET, PRIVADA
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_PAYMENT_METHOD_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "2";
            ///
            /// TODO: Pendiente validar como trabaja los meses intereses, si es por shipments en particular o en general.
            ///
            //Number of Installments - Posible values: 0, 3, 6, 12, 18
            merchantDefinedData.Value = paymentOrderProcessRequest.Shipments.FirstOrDefault() == null ? "0" : paymentOrderProcessRequest.Shipments.FirstOrDefault().ShippingPaymentInstallments;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "3";
            //Sale Channel - Posible values: WEB, APP
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderSaleChannel;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "4";
            //Num of days buyer is registered - Posible values: Empty
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerRegisteredDays;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "5";
            //Guest Buyer (Y/N) - Posible values: false
            merchantDefinedData.Value = "false";
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "6";
            //Number of purchase for customer - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerPurchasesQuantity;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "7";
            //Customer Contact Number - Posible values: 10 digits
            merchantDefinedData.Value = paymentOrderProcessRequest.CustomerContact;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "8";
            //Shipping DeadLine (Num Dias) - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "9";
            //Delivery Method - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.Shipments.Select(s => s.ShippingDeliveryDesc).FromEnumerableStringToString();
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "10";
            //Customer Loyality Number - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "11";
            //Promotional / Coupon Code - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderCouponCode;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "12";
            //Order Number - Posible values: variant
            merchantDefinedData.Value = paymentOrderProcessRequest.OrderReferenceNumber;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "13";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Bin - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BIN_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "14";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Bank - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_BANK_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "15";
            ///
            /// TODO: Se obtiene con el customerId y paymentToken
            ///
            //Type of Card - Posible values: calculated
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_TYPE_OF_CARD_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "16";
            //Loyalty Amount - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "17";
            //Wallets - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "18";
            //Second email - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "19";
            //Related consigment - Posible values: N/A
            merchantDefinedData.Value = (hasOriginalRequestOnlyMerchandiseType) ? PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD : paymentOrderProcessRequest.OrderReferenceNumber;
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "20";
            //Order Status - Posible values: N/A
            merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD;
            merchantDefinedDataList.Add(merchantDefinedData);
            //Shipments
            IList<Shipment> shipments = paymentOrderProcessRequest.Shipments.OrderBy(s => s.ShippingReferenceNumber).ToList();
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "21";
            if (shipments.FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 1 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.FirstOrDefault().ShippingReferenceNumber; //Consignment 1 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "22";
            if (shipments.Take(2).ToList().Count != 2 || shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 2 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingReferenceNumber; //Consignment 2 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "23";
            if (shipments.Take(3).ToList().Count != 3 || shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 3 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingReferenceNumber; //Consignment 3 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "24";
            if (shipments.Take(4).ToList().Count != 4 || shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 4 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingReferenceNumber; //Consignment 4 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "25";
            if (shipments.Take(5).ToList().Count != 5 || shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 5 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingReferenceNumber; //Consignment 5 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "26";
            if (shipments.Take(6).ToList().Count != 6 || shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = PaymentProcessorConstants.MERCHANT_DEFINED_DATA_NO_APPLY_FIELD; //Consignment 6 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingReferenceNumber; //Consignment 6 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "27";
            if (shipments.FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 1 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.FirstOrDefault().ShippingPaymentImport; //Consignment Amount 1 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "28";
            if (shipments.Take(2).ToList().Count != 2 || shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 2 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingPaymentImport; //Consignment Amount 2 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "29";
            if (shipments.Take(3).ToList().Count != 3 || shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 3 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingPaymentImport; //Consignment Amount 3 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "30";
            if (shipments.Take(4).ToList().Count != 4 || shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 4 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingPaymentImport; //Consignment Amount 4 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "31";
            if (shipments.Take(5).ToList().Count != 5 || shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 5 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingPaymentImport; //Consignment Amount 5 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "32";
            if (shipments.Take(6).ToList().Count != 6 || shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //Consignment Amount 6 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingPaymentImport; //Consignment Amount 6 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "33";
            if (shipments.FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 1 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.FirstOrDefault().ShippingStoreName; //StoreName 1 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "34";
            if (shipments.Take(2).ToList().Count != 2 || shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 2 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(2).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingStoreName; //StoreName 2 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "35";
            if (shipments.Take(3).ToList().Count != 3 || shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 3 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(3).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingStoreName; //StoreName 3 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "36";
            if (shipments.Take(4).ToList().Count != 4 || shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 4 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(4).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingStoreName; //StoreName 4 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "37";
            if (shipments.Take(5).ToList().Count != 5 || shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 5 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(5).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingStoreName; //StoreName 5 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            //
            merchantDefinedData = new MerchantDefinedData();
            merchantDefinedData.Key = "38";
            if (shipments.Take(6).ToList().Count != 6 || shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault() == null)
                merchantDefinedData.Value = string.Empty; //StoreName 6 - Posible values: variant
            else
                merchantDefinedData.Value = shipments.Take(6).OrderByDescending(s => s.ShippingReferenceNumber).FirstOrDefault().ShippingStoreName; //StoreName 6 - Posible values: variant
            merchantDefinedDataList.Add(merchantDefinedData);
            return merchantDefinedDataList;
        }
        private static bool HasOnlyGroceryMerchandiseType(PaymentOrderProcessRequest paymentOrderProcessRequest)
        {
            if (paymentOrderProcessRequest.Shipments.GroupBy(s => s.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1]).Count() == (int)MerchandiseTypeEnum.GROCERY
                && paymentOrderProcessRequest.Shipments.Any(s => Convert.ToByte(s.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1]) == (byte)MerchandiseTypeEnum.GROCERY))
                return true;
            return false;
        }
        private static bool HasOnlyNonGroceryMerchandiseType(PaymentOrderProcessRequest paymentOrderProcessRequest)
        {
            if (paymentOrderProcessRequest.Shipments.Where(s => Convert.ToByte(s.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1]) == (byte)MerchandiseTypeEnum.GROCERY).Any())
                return false;
            return true;
        }
        #endregion
        #region Public Methods
        public static byte GetMerchandiseType(PaymentOrderProcessRequest paymentOrderProcessRequest, string serviceInterface)
        {
            IsShippingReferenceNumberValid(paymentOrderProcessRequest, serviceInterface);
            IList<byte> shippingReferenceNumberIDList = paymentOrderProcessRequest.Shipments.Select(s => Convert.ToByte(s.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1])).OrderBy(s => s).ToList();
            byte merchandiseType = (shippingReferenceNumberIDList == null) ? (byte)0 : shippingReferenceNumberIDList.FirstOrDefault();
            if (merchandiseType == 0)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ConvertersConstants.MERCHANT_DEFINED_DATA_MERCHANDISE_DATA_TYPE,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = serviceInterface
                };
            if (shippingReferenceNumberIDList.FirstOrDefault() == (byte)MerchandiseTypeEnum.GROCERY)
                merchandiseType = (byte)MerchandiseTypeEnum.GROCERY;
            else
                merchandiseType = (byte)MerchandiseTypeEnum.NONGROCERY;
            return merchandiseType;
        }
        public static void IsShippingReferenceNumberValid(PaymentOrderProcessRequest paymentOrderProcessRequest, string serviceInterface)
        {
            IsPaymentOrderProcessRequestValid(paymentOrderProcessRequest, serviceInterface);
            if (paymentOrderProcessRequest.Shipments.Where(s => s.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR).Length != 2).Any())
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Format(ConvertersConstants.MERCHANT_DEFINED_DATA_FIELD_MAPPING, JsonFieldNamesConstants.SALESFORCE_PAYMENT_ORDER_PROCESS_SHIPPING_REFERENCE_NUMBER),
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = serviceInterface
                };
            if (paymentOrderProcessRequest.Shipments.Select(s => s.ShippingReferenceNumber.Trim().Split(CharactersConstants.HYPHEN_CHAR)[1].Trim()).Distinct().Count() != 1)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ConvertersConstants.MERCHANT_DEFINED_DATA_MERCHANDISE_TYPE,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = serviceInterface
                };
        }
        public static IList<MerchantDefinedData> GetMerchantDefinedData(PaymentOrderProcessRequest paymentOrderProcessRequest, PaymentOrderProcessRequest originalPaymentOrderProcessRequest, string serviceInterface)
        {
            IsShippingReferenceNumberValid(paymentOrderProcessRequest, serviceInterface);
            IsPaymentOrderProcessRequestValid(originalPaymentOrderProcessRequest, serviceInterface);
            byte merchandiseType = GetMerchandiseType(paymentOrderProcessRequest, serviceInterface);
            IList<MerchantDefinedData> merchantDefinedDataList = new List<MerchantDefinedData>();
            bool hasOriginalRequestOnlyMerchandiseType = false;
            if (merchandiseType == (byte)MerchandiseTypeEnum.GROCERY)
            {
                hasOriginalRequestOnlyMerchandiseType = HasOnlyGroceryMerchandiseType(originalPaymentOrderProcessRequest);
                merchantDefinedDataList = GetMerchantDefinedDataGrocery(paymentOrderProcessRequest, hasOriginalRequestOnlyMerchandiseType);
            }
            else
            {
                hasOriginalRequestOnlyMerchandiseType = HasOnlyNonGroceryMerchandiseType(originalPaymentOrderProcessRequest);
                merchantDefinedDataList = GetMerchantDefinedDataNonGrocery(paymentOrderProcessRequest, hasOriginalRequestOnlyMerchandiseType);
            }
            return merchantDefinedDataList;
        }
        #endregion
    }
}
