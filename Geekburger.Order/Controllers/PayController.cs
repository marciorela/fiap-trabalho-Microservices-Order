using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Contract.Messages;
using Geekburger.Order.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geekburger.Order.Controllers
{
    [ApiController]
    [Route("api")]
    public class PayController : ControllerBase
    {
        private readonly ILogger<PayController> _logger;
        private readonly PaymentService _paymentService;

        public PayController(ILogger<PayController> logger, PaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PostPay(PayRequest pay)
        {
            await _paymentService.RegisterPayment(pay);

            return Ok();
        }
    }
}