﻿using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Commands
{
    public class CheckStockCommand : ICommand
    {
        public string PlanId { get; set; }
    }
}