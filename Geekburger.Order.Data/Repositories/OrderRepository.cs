using Geekburger.Order.Database;
using GeekBurguer.UI.Contract;
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

    }
}
