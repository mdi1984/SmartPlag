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
  public class AssignmentController : Controller
  {
    private AssignmentManager manager;

    public AssignmentController(AssignmentManager manager)
    {
      this.manager = manager;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var assignments = manager.GetAssignments();
      var view = View(new AssignmentsModel { Assignments = assignments });
      return view;
    }

    [HttpGet]
    public IActionResult New()
    {
      var tokenizerServices = manager.GetTokenizerServices();
      var comparisonServices = manager.GetComparisonServices();
      var assignmentModel = new AssignmentModel
      {
        Assignment = new Assignment(),
        TokenizerServices = tokenizerServices,
        ComparisonServices = comparisonServices
      };

      return View("NewAssignment", assignmentModel);
    }

    [HttpPost]
    public IActionResult New(Assignment assignment)
    {
      this.manager.SaveOrUpdateAssignment(assignment);
      return Redirect("/assignment");
    }

    public IActionResult Edit(int id)
    {
      if(id == default(int))
        return BadRequest();

      var assignment = this.manager.GetAssignment(id);
      var tokenizerServices = manager.GetTokenizerServices();
      var comparisonServices = manager.GetComparisonServices();

      ViewBag.TokenizerServices = tokenizerServices;
      ViewBag.ComparisonServices = comparisonServices;



      return View("EditAssignment", assignment);
    }

    public IActionResult Evaluate(int id)
    {
      var assignment = this.manager.GetAssignment(id);
      var restTokenizer = new TokenizerManager(assignment.TokenizerService);
      restTokenizer.Tokenize(assignment);
      return Ok();
    }
  }

}
