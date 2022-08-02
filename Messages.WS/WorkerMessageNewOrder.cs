using Geekburger.Order.Contract.Messages;
using Geekburger.Order.Data.Repositories;
using GeekBurguer.UI.Contract;
using Messages.Service;
using Messages.Service.Messages;
using Messages.Service.Models;
using Newtonsoft.Json;
using System.Text;

namespace Messages.WS
{
    public class WorkerMessageNewOrder : BackgroundService
    {
        private readonly ILogger<WorkerMessageNewOrder> _logger;
        private readonly IConfiguration _config;
        private readonly OrderRepository _orderRepository;

        public WorkerMessageNewOrder(ILogger<WorkerMessageNewOrder> logger, IConfiguration config, OrderRepository orderRepository)
        {
            _logger = logger;
            _config = config;
            _orderRepository = orderRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var msg = new NewOrder()
            //{
            //    orderId = 4534,
            //    storeName = "paulista",
            //    total = 2132.65M,
            //    products = new List<ProductToGet>()
            //    {
            //        new ProductToGet() { ProductId = "121" },
            //        new ProductToGet() { ProductId = "42354" },
            //    },
            //    productionId = new List<int>()
            //    {
            //        343444,
            //        3433,
            //    }
            //};
            //var msgNewOrderPaulista = new MessageNewOrder("paulista");
            //await msgNewOrderPaulista.Send(msg);

            //var msg = new OrderChanged()
            //{
            //    OrderId = 3435,
            //    State = "Paid",
            //    StoreName = "paulista"
            //};

            //var msgOrderChanged = new MessageOrderChanged("paulista");
            //await msgOrderChanged.Send(msg);

            var message = new MessageNewOrder(_config.GetValue<string>("SubscriptionName"));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var readed = await message.Receive();
                if (readed != null)
                {
                    await Processar(readed);
                }

                await Task.Delay(200, stoppingToken);
            }
        }

        private async Task Processar(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var newOrder = JsonConvert.DeserializeObject<NewOrder>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (newOrder != null)
            {
                await _orderRepository.Add(newOrder);
            }
        }

    }
}