using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Order.Domain.Entities
{
    public class Payment
    {
        public int OrderId { get; set; }
        public int? RequesterId { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string? StoreName { get; set; }

        [Required]
        [StringLength(50)]
        public string? PayType { get; set; }

        [StringLength(16)]
        public string? CardNumber { get; set; }

        [StringLength(100)]
        public string? CardOwnerName { get; set; }

        [StringLength(3)]
        public string? SecurityCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(16)]
        public string? State { get; set; }
    }
}
