using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class PlaceOrderViewModel
    {
        [ScaffoldColumn(false)]
        public string OrderId { get; set; }
        public string Address { get; set; }
    }
}