using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Contract.Messages;
using Geekburger.Order.Data.Repositories;
using Messages.Service;
using Messages.Service.Messages;
using Microsoft.Extensions.Configuration;

namespace Geekburger.Order.Services
{
    public class PaymentService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IConfiguration _config;
        private static int _quantidade = 0;

        public PaymentService(OrderRepository orderRepository, IConfiguration config)
        {
            _orderRepository = orderRepository;
            _config = config;
        }

        public async Task RegisterPayment(PayRequest pay)
        {
            var state = EnumOrderState.Paid;

            var payment = await _orderRepository.GetPaymentById(pay.OrderId, pay.RequesterId);
            if (payment == null)
            {
                await _orderRepository.AddPayment(pay);
                state = CheckNewState(pay);
                await _orderRepository.UpdateOrderState(pay.OrderId, pay.RequesterId, state);
            }
            else
            {
                if (Enum.TryParse(payment.State, out EnumOrderState x))
                {
                    state = x;
                }
            }

            await SendMessage(pay, state);
        }

        private EnumOrderState CheckNewState(PayRequest pay)
        {
            if (++_quantidade > 4)
            {
                _quantidade = 0;
                return EnumOrderState.Canceled;
            };

            return EnumOrderState.Paid;
        }

        private async Task SendMessage(PayRequest pay, EnumOrderState state)
        {
            var order = await _orderRepository.GetById(pay.OrderId);

            var msgOrderChanged = new OrderChanged()
            {
                OrderId = pay.OrderId,
                StoreName = pay.StoreName,
                State = state.ToString(),
                Total = order is not null ? order.Total : 0
            };

            var msg = new MessageOrderChanged();
            await msg.Send(msgOrderChanged);
        }
    }
}