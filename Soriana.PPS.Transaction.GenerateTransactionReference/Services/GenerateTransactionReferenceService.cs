using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.DataAccess.PaymentProcess;
using System.Threading.Tasks;

namespace Soriana.PPS.Control.GenerateTransactionReference.Services
{
    public class GenerateTransactionReferenceService : IGenerateTransactionReferenceService
    {
        #region Private Fields
        private readonly ILogger<GenerateTransactionReferenceService> _Logger;
        private readonly IPaymentProcessContext _PaymentProcessContext;
        #endregion
        #region Constructors
        public GenerateTransactionReferenceService(IPaymentProcessContext paymentProcessContext,
                                                    ILogger<GenerateTransactionReferenceService> logger)
        {
            _Logger = logger;
            _PaymentProcessContext = paymentProcessContext;
        }
        #endregion
        #region Public Methods
        public async Task<TransactionReferenceResponse> GenerateTransactionReference()
        {
            return new TransactionReferenceResponse() { TransactionReferenceId = await _PaymentProcessContext.GetPaymentTransactionIDAsync() };
        }
        #endregion
    }
}
