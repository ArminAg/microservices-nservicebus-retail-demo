using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Handlers
{
    public class OrderBilledHandler : IHandleMessages<OrderBilledEvent>
    {
        static ILog logger = LogManager.GetLogger<OrderBilledHandler>();

        public Task Handle(OrderBilledEvent message, IMessageHandlerContext context)
        {
            logger.Info($"Received OrderBilledEvent, OrderId = { message.OrderId } - Shipping order...");

            var shippingProcessedEvent = new ShippingProcessedEvent
            {
                OrderId = message.OrderId
            };

            return context.Publish(shippingProcessedEvent);
        }
    }
}
