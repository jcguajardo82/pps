using System;
using System.Collections.Generic;
using System.Text;

namespace Soriana.PPS.Common.DTO.ClosureOrder
{
    public class GetTransactionByOrder
    {
        public string PaymentTransactionID { get; set; }
        public string PaymentOrderID { get; set; }
        public string OrderReferenceNumber { get; set; }
        public string TransactionStatusIDSequence { get; set; }
        public string TransactionStatus { get; set; }
    }
}
