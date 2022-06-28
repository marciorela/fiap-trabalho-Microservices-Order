using Geekburger.Order.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Order.Contract.Messages
{
    public class OrderChanged
    {
        public int OrderId { get; set; }

        public string StoreName { get; set; }

        public EnumOrderState State { get; set; }
    }
}
