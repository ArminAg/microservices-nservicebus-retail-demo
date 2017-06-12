using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public class OrderPlacedEvent : IEvent
    {
        public string OrderId { get; set; }
    }
}
