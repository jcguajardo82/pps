namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class TransactionResponse
    {
        #region Public Properties
        public bool IsValid { get; set; }
        public string Status { get; set; }
        public string ReasonCodes { get; set; }
        public object ResponseObject { get; set; }
        public string TransactionAuthorizationId { get; set; }
        #endregion
    }
}
