using Geekburger.Order.Domain.Messages;
using GeekBurguer.UI.Contracts.Commands.Input;
using Messages.Service.Messages;


var orderId = Guid.NewGuid();
var msg = new MessageNewOrder();
await msg.Send(new NewOrder()
{
    OrderId = orderId,
    StoreName = "morumbi",
    ProductionIds = new() { 
        Guid.NewGuid(), 
        Guid.NewGuid(), 
        Guid.NewGuid(), 
    },
    Products = new() {
        Guid.NewGuid(),
        Guid.NewGuid(),
    }    
});

Console.WriteLine(orderId);
