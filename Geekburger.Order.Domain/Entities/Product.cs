using System.ComponentModel.DataAnnotations;

namespace Geekburger.Order.Domain.Entities
{
    public class Product
    {
        public string? ProductId { get; set; }
    
        [Required]
        public int OrderId { get; set; }
    }
}
