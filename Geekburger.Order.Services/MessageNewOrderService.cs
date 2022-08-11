using Geekburger.Order.Data.Repositories;
using GeekBurguer.UI.Contract;
using Messages.Service.Messages;
using Messages.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Order.Services
{
    public static class MessageNewOrderService
    {
        private static IServiceProvider? _services;

        public async static Task<IApplicationBuilder> HandleMessageNewOrder(this IApplicationBuilder app)
        {
            _services = app.ApplicationServices;

            var message = new MessageNewOrder();
            await message.Receive(ProcessaNewOrder);

            return app;
        }

        private async static Task ProcessaNewOrder(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var userWithLessOffer = JsonConvert.DeserializeObject<NewOrder>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (userWithLessOffer is not null)
            {
                var factory = _services?.GetService<IServiceScopeFactory>();

                if (factory is not null)
                {
                    using var scope = factory.CreateScope();

                    var _orderRepository = scope.ServiceProvider.GetService<OrderRepository>();
                    if (_orderRepository is not null)
                    {
                        if (_orderRepository is not null)
                        {
                            await _orderRepository.Add(userWithLessOffer);
                        }
                    }
                }
            }
        }
    }
}
