using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Manager.Simple.EF;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class AssignmentsController : Controller
  {
    private AssignmentManager manager;

    public AssignmentsController(AssignmentManager manager)
    {
      this.manager = manager;
    }

    [Authorize]
    public IActionResult Index()
    {

      return View();
    }
  }
}
