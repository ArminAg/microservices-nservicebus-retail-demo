using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        private IEndpointInstance endpointInstance;

        public OrdersController(IEndpointInstance endpointInstance)
        {
            this.endpointInstance = endpointInstance;
        }
        // GET: api/Orders
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        public async Task<string> Get(int id)
        {
            await endpointInstance.Send("Sales", new PlaceOrderCommand
            {
                OrderId = Guid.NewGuid().ToString()
            });
            return "value";
        }

        // POST: api/Orders
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Orders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }
    }
}
