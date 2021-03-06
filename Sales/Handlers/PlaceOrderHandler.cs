﻿using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Handlers
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrderCommand>
    {
        // The entries written with the logger will appear in the log file in addition to the console
        // Because LogManager.GetLogger(..); is an expensive call, it's important to always implement loggers as static members
        static ILog logger = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Received PlaceOrderCommand, OrderId = { message.OrderId }");
            // Task.CompletedTask requires .NET 4.6.1

            // This is normally where some business logic would occur

            var orderPlaced = new OrderPlacedEvent
            {
                OrderId = message.OrderId
            };
            return context.Publish(orderPlaced);
        }
    }
}
