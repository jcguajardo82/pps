﻿namespace Soriana.PPS.Common.Constants
{
    public static class JsonSchemaConstants
    {
        #region Constants
        public const string SALESFORCE_ORDER_PAYMENT_ORDER_PROCESS_SCHEMA = "{'$schema':'http://json-schema.org/draft-04/schema#','type':'object','properties':{'orderReferenceNumber':{'type':'string'},'orderDateTime':{'type':'string'},'orderSaleChannel':{'type':'string'},'orderCouponCode':{'type':'string'},'orderAmount':{'type':'string'},'paymentType':{'type':'string'},'paymentProcessor':{'type':'string'},'paymentToken':{'type':'string'},'paymentCardCVV':{'type':'string'},'paymentCardNIP':{'type':'string'},'paymentSaveCard':{'type':'string'},'customerEmail':{'type':'string'},'customerFirstName':{'type':'string'},'customerLastName':{'type':'string'},'customerAddress':{'type':'string'},'customerCity':{'type':'string'},'customerState':{'type':'string'},'customerZipCode':{'type':'string'},'customerCountry':{'type':'string'},'customerId':{'type':'string'},'customerDeviceFingerPrintId':{'type':'string'},'customerIPAddress':{'type':'string'},'customerPurchasesQuantity':{'type':'string'},'customerContact':{'type':'string'},'customerLoyaltyCardId':{'type':'string'},'customerLoyaltyRedeemElectronicMoney':{'type':'string'},'customerLoyaltyRedeemPoints':{'type':'string'},'customerLoyaltyRedeemMoney':{'type':'string'},'customerRegisteredDays':{'type':'string'},'shipments':{'type':'array','items':[{'type':'object','properties':{'shippingReferenceNumber':{'type':'string'},'shippingStoreId':{'type':'string'},'shippingStoreName':{'type':'string'},'shippingDeliveryId':{'type':'string'},'shippingDeliveryDesc':{'type':'string'},'shippingPaymentInstallments':{'type':'string'},'shippingPaymentImport':{'type':'string'},'shippingFirstName':{'type':'string'},'shippingLastName':{'type':'string'},'shippingAddress':{'type':'string'},'shippingCity':{'type':'string'},'items':{'type':'array','items':[{'type':'object','properties':{'shippingItemId':{'type':'string'},'shippingItemEAN':{'type':'string'},'shippingItemName':{'type':'string'},'shippingItemCategory':{'type':'string'},'shippingItemPrice':{'type':'number'},'shippingItemQuantity':{'type':'integer'},'shippintItemTotal':{'type':'number'}},'required':['shippingItemId','shippingItemEAN','shippingItemName','shippingItemCategory','shippingItemPrice','shippingItemQuantity','shippintItemTotal']}]}},'required':['shippingReferenceNumber','shippingStoreId','shippingStoreName','shippingDeliveryId','shippingDeliveryDesc','shippingPaymentInstallments','shippingPaymentImport','shippingFirstName','shippingLastName','shippingAddress','shippingCity','items']}]}},'required':['orderReferenceNumber','orderDateTime','orderSaleChannel','orderCouponCode','orderAmount','paymentType','paymentProcessor','paymentToken','paymentCardCVV','paymentCardNIP','paymentSaveCard','customerEmail','customerFirstName','customerLastName','customerAddress','customerCity','customerState','customerZipCode','customerCountry','customerId','customerDeviceFingerPrintId','customerIPAddress','customerPurchasesQuantity','customerContact','customerLoyaltyCardId','customerLoyaltyRedeemElectronicMoney','customerLoyaltyRedeemPoints','customerLoyaltyRedeemMoney','customerRegisteredDays','shipments']}";
        #endregion
    }
}
