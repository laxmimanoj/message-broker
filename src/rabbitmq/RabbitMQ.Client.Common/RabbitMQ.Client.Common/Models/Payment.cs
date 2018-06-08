using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Client.Common.Models
{
    [Serializable]
    public class Payment
    {
        public decimal AmountToPay;
        public string CardNumber;
        public string Name;
    }
}
