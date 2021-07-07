using System;
using System.Collections.Generic;
using System.Text;

namespace Soriana.PPS.Common.DTO.ClosureOrder
{
    public class CobroXMLResponse
    {
		public string PaymentOrderID { get; set; }
		public string OrderReferenceNumber { get; set; }
		public string PaymentProcessor { get; set; }
		public string PaymentToken { get; set; }
		public string PaymentSaveCard { get; set; }
		public string CustomerLoyaltyCardId { get; set; }
		public string PaymentType { get; set; }

	}
}
