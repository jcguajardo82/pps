namespace Soriana.PPS.Common.Enums
{
    public enum ProcessPaymentStatus400ReasonCodesEnum
    {
        MISSING_FIELD = 0,
        INVALID_DATA = 1,
        DUPLICATE_REQUEST = 2,
        INVALID_CARD = 3,
        CARD_TYPE_NOT_ACCEPTED = 4,
        INVALID_MERCHANT_CONFIGURATION = 5,
        PROCESSOR_UNAVAILABLE = 6,
        INVALID_AMOUNT = 7,
        INVALID_CARD_TYPE = 8,
        INVALID_PAYMENT_ID = 9,
        DEBIT_CARD_USEAGE_EXCEEDD_LIMIT = 10,
    }
}
