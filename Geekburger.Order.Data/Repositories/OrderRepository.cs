using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Database;
using Geekburger.Order.Domain.Entities;
using Geekburger.Order.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Order.Data.Repositories
{
    public class OrderRepository : RepositoryBase
    {
        public OrderRepository(OrderDbContext ctx) : base(ctx)
        {
        }

        public async Task Add(NewOrder order)
        {
            // GRAVA OS DADOS DA ORDEM
            await _ctx.Orders.AddAsync(new Domain.Entities.Order()
            {
                OrderId = order.OrderId,
                StoreName = order.StoreName,
                Total = new Random(100000).Next() //TODO: ESTÁ FALTANDO O TOTAL (double)order.Total
            });

            // GRAVA A LISTA DE PRODUTOS
            var product = order.products.Select(p => new Domain.Entities.Product()
            {
                OrderId = order.OrderId,
                ProductId = p.ProductId
            });
            await _ctx.OrdersProducts.AddRangeAsync(product);

            // GRAVA A LISTA DE PRODUCTIONS
            var productions = order.ProductionIds.Select(p => new Domain.Entities.Production()
            {
                OrderId = order.OrderId,
                ProductionId = p
            });
            await _ctx.OrdersProduction.AddRangeAsync(productions);

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateOrderState(int orderId, int requesterId, EnumOrderState state)
        {
            var payment = await GetPaymentById(orderId, requesterId);
            if (payment != null)
            {
                payment.State = state.ToString();
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Payment?> GetPaymentById(int orderId, int requesterId)
        {
            return await _ctx.OrdersPayments.FirstOrDefaultAsync(p => p.OrderId == orderId && p.RequesterId == requesterId);
        }

        public async Task<Domain.Entities.Order?> GetById(int orderId)
        {
            return await _ctx.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment> AddPayment(PayRequest pay)
        {
            var payment = new Domain.Entities.Payment()
            {
                CardNumber = pay.CardNumber,
                CardOwnerName = pay.CardOwnerName,
                ExpirationDate = pay.ExpirationDate,
                PayType = pay.PayType,
                SecurityCode = pay.SecurityCode,
                StoreName = pay.StoreName,
                OrderId = pay.OrderId,
                RequesterId = pay.RequesterId
            };

            await _ctx.OrdersPayments.AddAsync(payment);
            await _ctx.SaveChangesAsync();

            return payment;
        }

        public async Task<bool> PaymentExists(int orderId, int requesterId)
        {
            return await GetPaymentById(orderId, requesterId) != null;
        }
    }
}
