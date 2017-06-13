using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planning.Sagas
{
    public class PlanningSagaData : ContainSagaData
    {
        public string PlanId { get; set; }
    }
}