using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Contract.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Geekburger.Order.Controllers
{
    [ApiController]
    [Route("api")]
    public class PayController : ControllerBase
    {
        private readonly ILogger<PayController> _logger;
        private static int _quantidade = 0;

        public PayController(ILogger<PayController> logger)
        {
            _logger = logger;
        }

        [HttpPost("pay")]
        public IActionResult PostPay(PayRequest pay)
        {
            var msgOrderChanged = new OrderChanged()
            {
                OrderId = pay.OrderId,
                StoreName = pay.StoreName,
                State = EnumOrderState.Paid
            };

            if (++_quantidade > 4)
            {
                _quantidade = 0;
                msgOrderChanged.State = EnumOrderState.Canceled;
            };




            return Ok();
        }
    }
}