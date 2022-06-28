using Geekburger.Order.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Geekburger.Order.Controllers
{
    [ApiController]
    [Route("api")]
    public class PayController : ControllerBase
    {
        private readonly ILogger<PayController> _logger;

        public PayController(ILogger<PayController> logger)
        {
            _logger = logger;
        }

        [HttpPost("pay")]
        public IActionResult Post(PayRequest pay)
        {
            return Ok();
        }
    }
}