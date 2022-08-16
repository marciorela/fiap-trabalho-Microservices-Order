using System;

namespace Geekburger.Order.Contract.DTOs
{

    public class PayRequest
    {
        public Guid OrderId { get; set; }
        public string StoreName { get; set; }
        public string PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid RequesterId { get; set; }
    }
}
