using Geekburger.Order.Domain.Messages;
using GeekBurguer.UI.Contracts.Commands.Input;
using Messages.Service.Messages;


var msg = new MessageNewOrder();
await msg.Send(new NewOrder()
{
    OrderId = 1002,
    StoreName = "morumbi",
    ProductionIds = new() { 1, 2, 3, 4 },
    products = new() {
            new InputProductCommand() { ProductId = 8989 },
            new InputProductCommand() { ProductId = 1233 },
            new InputProductCommand() { ProductId = 1234 },
            new InputProductCommand() { ProductId = 1235 },
        }
});
