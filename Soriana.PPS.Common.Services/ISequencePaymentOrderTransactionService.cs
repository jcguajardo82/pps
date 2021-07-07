using Soriana.PPS.Common.DTO.Salesforce;
using System.Collections.Generic;

namespace Soriana.PPS.Common.Services
{
    public interface ISequencePaymentOrderTransactionService
    {
        IList<PaymentOrderProcessRequest> GetSequencePaymentOrderTransaction(params PaymentOrderProcessRequest[] request);
    }
}
