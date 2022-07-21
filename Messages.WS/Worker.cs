using Geekburger.Order.Contract.Messages;
using Messages.Service;
using Messages.Service.Models;
using Newtonsoft.Json;
using System.Text;

namespace Messages.WS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private List<string> lojas = new();
        private List<Message> messages = new();

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            lojas.Add("paulista_store");
            lojas.Add("morumbi_store");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var msg = new OrderChanged()
            {
                OrderId = 122,
                State = Geekburger.Order.Contract.Enums.EnumOrderState.Paid,
                StoreName = "paulista"
            };

            var msgNewOrderPaulista = new MessageNewOrder("paulista_store");
            await msgNewOrderPaulista.Send(msg);

            foreach (var loja in lojas)
            {
                messages.Add(new MessageNewOrder(loja));
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                foreach (var message in messages)
                {
                    var readed = await message.Receive();
                    if (readed != null)
                    {
                        message.Process(readed);
                    }

                }

                await Task.Delay(200, stoppingToken);
            }
        }

    }
}