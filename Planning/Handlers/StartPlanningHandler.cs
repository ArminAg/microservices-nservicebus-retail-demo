using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace Planning.Handlers
{
    public class StartPlanningHandler : IHandleMessages<StartPlanningCommand>
    {
        static ILog logger = LogManager.GetLogger<StartPlanningHandler>();

        public Task Handle(StartPlanningCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Received StartPlanningCommand, PlanId = { message.PlanId }, Planning Started");
            return Task.CompletedTask;
        }
    }
}