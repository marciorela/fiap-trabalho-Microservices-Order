using System.ComponentModel.DataAnnotations;

namespace Geekburger.Order.Domain.Entities
{
    public class Production
    {
        public Guid ProductionId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

    }
}