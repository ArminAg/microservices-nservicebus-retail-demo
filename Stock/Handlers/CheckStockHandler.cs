using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Messages;

namespace Stock.Handlers
{
    public class CheckStockHandler : IHandleMessages<CheckStockCommand>
    {
        static ILog logger = LogManager.GetLogger<CheckStockHandler>();

        public async Task Handle(CheckStockCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Checking Stock for PlanId = { message.PlanId }");
            // Do checking
            await context.Reply<IStockCheckedMessage>(msg => { }).ConfigureAwait(false);
        }
    }
}