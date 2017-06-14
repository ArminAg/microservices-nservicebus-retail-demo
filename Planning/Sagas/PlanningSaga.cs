using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Messages;

namespace Planning.Sagas
{
    public class PlanningSaga : Saga<PlanningSagaData>,
        IAmStartedByMessages<StartPlanningCommand>,
        IHandleMessages<IStockCheckedMessage>,
        IHandleMessages<IOrderedItemsMessage>

    {
        static ILog logger = LogManager.GetLogger<PlanningSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PlanningSagaData> mapper)
        {
            mapper.ConfigureMapping<StartPlanningCommand>(message => message.PlanId)
                .ToSaga(sagaData => sagaData.PlanId);
        }

        public async Task Handle(StartPlanningCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Planning is started for PlanId = { message.PlanId }");
            Data.PlanId = message.PlanId;
            await context.Send("Stock", new CheckStockCommand
            {
                PlanId = message.PlanId
            });
        }

        public async Task Handle(IStockCheckedMessage message, IMessageHandlerContext context)
        {
            logger.Info($"Stock was checked for PlanId = { Data.PlanId }");
            await context.Send("Order", new OrderItemsCommand
            {
                PlanId = Data.PlanId
            });
        }

        public async Task Handle(IOrderedItemsMessage message, IMessageHandlerContext context)
        {
            logger.Info("Items Have been ordered! Notifying originator and ending saga.");
            await ReplyToOriginator(context, new PlanningProcessedMessage
            {
                PlanId = Data.PlanId
            }).ConfigureAwait(false);
            MarkAsComplete();
        }
    }
}