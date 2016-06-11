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
    private PlagContext context;

    public AssignmentsController(PlagContext context)
    {
      this.context = context;
    }

    public IActionResult Index()
    {
      var assignments = context.Assignments.ToList();
      var view = View(new AssignmentsModel {Assignments = assignments});
      return view;
    }

    public IActionResult NewAssignment()
    {
      return View("NewAssignment", new Assignment());
    }

    [HttpPost]
    public IActionResult NewAssignment(Assignment assignment)
    {
      this.context.Assignments.Add(assignment);
      this.context.SaveChanges();
      return Redirect("/assignments");
    }
  }

}
