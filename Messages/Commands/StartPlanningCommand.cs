using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Commands
{
    public class StartPlanningCommand : ICommand
    {
        public string PlanId { get; set; }
        public string PlanName { get; set; }
    }
}
