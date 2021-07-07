namespace Soriana.PPS.Common.Enums
{
    public enum CapturePaymentStatus400ReasonCodesEnum
    {
        MISSING_FIELD = 0,
        INVALID_DATA = 1,
        DUPLICATE_REQUEST = 2,
        INVALID_MERCHANT_CONFIGURATION = 3,
        EXCEEDS_AUTH_AMOUNT = 4,
        AUTH_ALREADY_REVERSED = 5,
        TRANSACTION_ALREADY_SETTLED = 6,
        INVALID_AMOUNT = 7,
        MISSING_AUTH = 8,
        TRANSACTION_ALREADY_REVERSED_OR_SETTLED = 9,
    }
}
