using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEndpointInstance endpointInstance;

        public HomeController(IEndpointInstance endpointInstance)
        {
            this.endpointInstance = endpointInstance;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(PlaceOrderViewModel viewModel)
        {
            await endpointInstance.Send("Sales", new PlaceOrderCommand
            {
                OrderId = Guid.NewGuid().ToString()
            });

            return View(viewModel);
        }
    }
}