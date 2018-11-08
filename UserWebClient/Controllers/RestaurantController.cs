using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserWebClient.Controllers
{
    public class RestaurantController : ApiController
    {
        // GET: api/Restaurant
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Restaurant/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Restaurant
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Restaurant/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Restaurant/5
        public void Delete(int id)
        {
        }
    }
}
