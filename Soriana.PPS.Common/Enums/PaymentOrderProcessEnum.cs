namespace Soriana.PPS.Common.Enums
{
    public enum PaymentOrderProcessEnum
    {
        CreateDecision = 1,
        ProcessPayment = 2,
        CapturePayment = 3,
        Enrollment = 4,
        ValidateAuthentication = 5,
        NotifyAuthentication = 6,
        GenerateToken = 7,
        GenerateTransactionReference = 8
    }
}
