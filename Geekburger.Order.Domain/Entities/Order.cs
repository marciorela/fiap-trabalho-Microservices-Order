using System.ComponentModel.DataAnnotations;

namespace Geekburger.Order.Domain.Entities
{
    public class Order
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string? StoreName { get; set; }
        
        [Required]
        public double Total { get; set; }

        public List<Product> Products { get; set; } = new();
        public List<Production> Productions { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}
