using System.ComponentModel.DataAnnotations;

namespace Geekburger.Order.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
    
        [Required]
        public Guid OrderId { get; set; }
    }
}
