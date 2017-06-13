using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace Planning.Sagas
{
    public class PlanningSaga : Saga<PlanningSagaData>,
        IAmStartedByMessages<StartPlanningCommand>

    {
        static ILog logger = LogManager.GetLogger<PlanningSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PlanningSagaData> mapper)
        {
            mapper.ConfigureMapping<StartPlanningCommand>(message => message.PlanId)
                .ToSaga(sagaData => sagaData.PlanId);
        }

        public Task Handle(StartPlanningCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Planning is started for PlanId = { message.PlanId }");
            return Task.CompletedTask;
        }
    }
}