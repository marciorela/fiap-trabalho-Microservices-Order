using GeekBurguer.UI.Contracts.Commands.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Order.Domain.Messages
{
    public class NewOrder
    {
        public Guid OrderId { get; set; }

        public string? StoreName { get; set; }

        public List<Guid> Products { get; set; } = new();

        public List<Guid> ProductionIds { get; set; } = new();
    }
}
