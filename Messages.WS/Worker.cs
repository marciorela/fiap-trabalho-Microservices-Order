using Geekburger.Order.Contract.Messages;
using Messages.Service;

namespace Messages.WS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var msg = new OrderChanged()
            {
                OrderId = 122,
                State = Geekburger.Order.Contract.Enums.EnumOrderState.Paid,
                StoreName = "paulista"
            };

            var mensagem = new Message();
            await mensagem.Send("orderchanged", msg);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}