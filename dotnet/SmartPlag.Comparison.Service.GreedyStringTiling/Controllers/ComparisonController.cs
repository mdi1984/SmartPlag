using System;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model;
namespace SmartPlag.Comparison.Service.GreedyStringTiling.Controllers
{
  [Route("api/[controller]")]
  public class ComparisonController
  {
    [HttpPost]
    public IActionResult SimpleComparison([FromBody] ComparisonRequest request)
    {
      try
      {
        return new ObjectResult(null);
      }
      catch (Exception)
      {
        return new BadRequestObjectResult("Invalid request format");
      }
    }
  }
}