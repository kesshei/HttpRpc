using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetResult")]
        public string GetResult(string name)
        {
            return $"服务端返回:{name}";
        }
        [HttpPost("Result")]
        public TestModel Result(TestModel testModel)
        {
            testModel.Msg = "服务器";
            return testModel;
        }
    }
}
