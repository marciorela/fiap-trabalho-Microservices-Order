using Geekburger.Extensions;
using Geekburger.Order.Data.Repositories;
using Geekburger.Order.Domain.Messages;
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
            var newOrder = JsonConvert.DeserializeObject<NewOrder>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (newOrder is not null && _services is not null)
            {
                await _services.ExecuteAsync<OrderRepository>(async (_orderRepository) =>
                {
                    if (await _orderRepository.GetById(newOrder.OrderId) is null)
                    {
                        await _orderRepository.Add(newOrder);
                    }
                });
            }
        }
    }
}
