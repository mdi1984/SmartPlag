using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartPlag.Manager.SimpleManager.Controllers.API
{
  [Route("api/test")]
  public class ApiTestController
  {
    [HttpGet]

    public IActionResult Test()
    {
      var retObj = new
      {
        Test = "sup",
      };

      return new OkObjectResult(retObj);
    }
  }
}
