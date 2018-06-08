using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Client.Common.Models
{
    [Serializable]
    public class PurchaseOrder
    {
        public decimal AmountToPay;
        public string PoNumber;
        public string CompanyName;
        public int PaymentDayTerms;
    }
}
