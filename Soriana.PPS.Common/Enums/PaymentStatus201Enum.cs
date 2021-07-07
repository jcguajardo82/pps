namespace Soriana.PPS.Common.Enums
{
    public enum PaymentStatus201Enum
    {
        AUTHORIZED = 0,
        PARTIAL_AUTHORIZED = 1,
        AUTHORIZED_PENDING_REVIEW = 2,
        AUTHORIZED_RISK_DECLINED = 3,
        PENDING_AUTHENTICATION = 4,
        PENDING_REVIEW = 5,
        DECLINED = 6,
        INVALID_REQUEST = 7
    }
}
