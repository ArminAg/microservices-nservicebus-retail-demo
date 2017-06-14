using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Handlers
{
    public class PlanningProcessedHandler : IHandleMessages<PlanningProcessedMessage>
    {
        static ILog logger = LogManager.GetLogger<PlanningProcessedHandler>();

        public Task Handle(PlanningProcessedMessage message, IMessageHandlerContext context)
        {
            logger.Info($"Notifying shipping that Saga for Planning is finished, PlanId = { message.PlanId }");
            return Task.CompletedTask;
        }
    }
}
