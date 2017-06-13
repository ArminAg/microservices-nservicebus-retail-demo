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
    public class PlannerController : ApiController
    {
        private IEndpointInstance endpointInstance;

        public PlannerController(IEndpointInstance endpointInstance)
        {
            this.endpointInstance = endpointInstance;
        }

        // GET: api/Planner
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Planner/5
        public async Task<string> Get(int id)
        {
            await endpointInstance.Send("Planning", new StartPlanningCommand
            {
                PlanId = Guid.NewGuid().ToString(),
                PlanName = "Test Plan"
            });
            return "value";
        }

        // POST: api/Planner
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Planner/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Planner/5
        public void Delete(int id)
        {
        }
    }
}
