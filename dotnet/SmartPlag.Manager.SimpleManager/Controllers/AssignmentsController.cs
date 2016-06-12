using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.SimpleManager.Controllers.API;
using SmartPlag.Manager.SimpleManager.Infrastructure;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;
using SmartPlag.Manager.SimpleManager.Model;


namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class AssignmentsController : Controller
  {
    private AssignmentManager manager;

    public AssignmentsController(AssignmentManager manager)
    {
      this.manager = manager;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var assignments = manager.GetAssignments();
      var view = View(new AssignmentsModel {Assignments = assignments});
      return view;
    }

    [HttpGet]
    public IActionResult NewAssignment()
    {
      return View("NewAssignment", new Assignment());
    }

    [HttpPost]
    public IActionResult NewAssignment(Assignment assignment)
    {
      this.manager.SaveOrUpdateAssignment(assignment);
      return Redirect("/assignments");
    }
  }

}
