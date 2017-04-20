using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAppAsyncDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task< IEnumerable<string>> Get()
        {
            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> task = httpClient.GetAsync("http://www.microsoft.com");
            
            var httpResponseMessage = await task;
            Debug.Assert(httpResponseMessage.IsSuccessStatusCode);

            return new string[] { "value1", "value2", httpResponseMessage.StatusCode.ToString() };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
