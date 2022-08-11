namespace Geekburger.Order.Contract.Messages
{
    public class OrderChanged
    {
        public int OrderId { get; set; }

        public string StoreName { get; set; }

        public string State { get; set; }

        public double Total { get; set; }
    }
}
