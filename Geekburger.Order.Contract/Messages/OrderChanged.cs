using System;

namespace Geekburger.Order.Contract.Messages
{
    public class OrderChanged
    {
        public Guid OrderId { get; set; }

        public string StoreName { get; set; }

        public string State { get; set; }

        public double Total { get; set; }
    }
}
