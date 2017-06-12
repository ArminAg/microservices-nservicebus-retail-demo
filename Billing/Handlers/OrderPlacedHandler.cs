using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Handlers
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlacedEvent>
    {
        static ILog logger = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlacedEvent message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderPlacedEvent, OrderId = { message.OrderId } - Charging credit card..");
            return Task.CompletedTask;
        }
    }
}
