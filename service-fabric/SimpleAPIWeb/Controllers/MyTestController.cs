using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SimpleAPIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyTestController : ControllerBase
    {
        private readonly ILogger<MyTestController> _logger;

        public MyTestController(ILogger<MyTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TestData> Get()
        {
            return new TestData[] { new TestData() };
        }
    }
}
