using Geekburger.Order.Contract.DTOs;
using Geekburger.Order.Contract.Enums;
using Geekburger.Order.Database;
using Geekburger.Order.Domain.Entities;
using GeekBurguer.UI.Contract;
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
                OrderId = order.orderId,
                StoreName = order.storeName,
                Total = (double)order.total
            });

            // GRAVA A LISTA DE PRODUTOS
            var products = order.products.Select(p => new Domain.Entities.Product()
            {
                OrderId = order.orderId,
                ProductId = p.ProductId
            });
            await _ctx.OrdersProducts.AddRangeAsync(products);

            // GRAVA A LISTA DE PRODUCTIONS
            var productions = order.productionId.Select(p => new Domain.Entities.Production()
            {
                OrderId = order.orderId,
                ProductionId = p
            });
            await _ctx.OrdersProduction.AddRangeAsync(productions);

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateOrderState(int orderId, int requesterId, EnumOrderState state)
        {
            var payment = await GetById(orderId, requesterId);
            if (payment != null)
            {
                payment.State = state.ToString();
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Payment?> GetById(int orderId, int requesterId)
        {
            return await _ctx.OrdersPayments.FirstOrDefaultAsync(p => p.OrderId == orderId && p.RequesterId == requesterId);
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
            return await GetById(orderId, requesterId) != null;
        }
    }
}
