using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Messages;

namespace Order.Handlers
{
    public class OrderItemsHandler : IHandleMessages<OrderItemsCommand>
    {
        static ILog logger = LogManager.GetLogger<OrderItemsHandler>();
        
        public async Task Handle(OrderItemsCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderItemsCommand for PlanId = { message.PlanId }");
            await context.Reply<IOrderedItemsMessage>(e => { }).ConfigureAwait(false);
        }
    }
}