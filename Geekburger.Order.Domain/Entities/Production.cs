using System.ComponentModel.DataAnnotations;

namespace Geekburger.Order.Domain.Entities
{
    public class Production
    {
        public int ProductionId { get; set; }

        [Required]
        public int OrderId { get; set; }

    }
}