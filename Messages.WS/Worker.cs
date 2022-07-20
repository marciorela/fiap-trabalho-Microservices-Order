using Geekburger.Order.Contract.Messages;
using Messages.Service;
using Newtonsoft.Json;
using System.Text;

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

            var mensagem = new MessageNewOrder();
            await mensagem.Send(msg);

            while (!stoppingToken.IsCancellationRequested)
            {
                var readed = await mensagem.Receive();
                if (readed != null)
                {
                    ProcessaMensagem(readed);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
            }
        }

        private void ProcessaMensagem(MessageBase obj)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(obj.MessageId.ToString());

            var x = Encoding.UTF8.GetString(obj.Body);
            var y = JsonConvert.DeserializeObject<OrderChanged>(x);

            Console.WriteLine(y.OrderId);
            Console.WriteLine(y.State);
            Console.WriteLine(y.StoreName);

        }
    }
}