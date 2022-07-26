﻿using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Contract.Messages;
using Geekburger.Order.Data.Repositories;
using Messages.Service;

namespace Geekburger.Order.Services
{
    public class PaymentService
    {
        private readonly OrderRepository _orderRepository;
        private static int _quantidade = 0;

        public PaymentService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task RegisterPayment(PayRequest pay)
        {
            var state = EnumOrderState.Paid;

            var payment = await _orderRepository.GetById(pay.OrderId, pay.RequesterId);
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
            var msgOrderChanged = new OrderChanged()
            {
                OrderId = pay.OrderId,
                StoreName = pay.StoreName,
                State = state.ToString()
            };

            var msg = new MessageOrderChanged(pay.StoreName);
            await msg.Send(msgOrderChanged);
        }
    }
}