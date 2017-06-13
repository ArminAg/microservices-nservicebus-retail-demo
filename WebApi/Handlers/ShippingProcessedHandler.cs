using Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace WebApi.Handlers
{
    public class ShippingProcessedHandler : IHandleMessages<ShippingProcessedEvent>
    {
        static ILog logger = LogManager.GetLogger<ShippingProcessedHandler>();

        public Task Handle(ShippingProcessedEvent message, IMessageHandlerContext context)
        {
            logger.Info($"Received ShippingProcessedEvent, OrderId = { message.OrderId }, Doing some additional operations...");
            return Task.CompletedTask;
        }
    }
}