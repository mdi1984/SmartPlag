using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Model;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  [Authorize]
  public class AssignmentsController : Controller
  {
    private AssignmentManager manager;

    public AssignmentsController(AssignmentManager manager)
    {
      this.manager = manager;
    }

    public async Task<IActionResult> Index()
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        ViewBag.Username = user;
        var assignments = await this.manager.GetAssignmentsAsync(user);

        return View(new AssignmentListModel
        {
          Assignments = assignments.ToList()
        });
      }

      return BadRequest();
    }

    public async Task<IActionResult> Create()
    {
      var tokenizers = await manager.GetTokenizerServicesAsync();
      var comparators = await manager.GetComparisonServicesAsync();

      ViewBag.Tokenizers = tokenizers.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
      ViewBag.Comparators = comparators.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();

      return View("Edit", new EditAssignmentModel());
    }

    public async Task<IActionResult> Edit(int id)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var tokenizers = await manager.GetTokenizerServicesAsync();
        var comparators = await manager.GetComparisonServicesAsync();

        ViewBag.Tokenizers = tokenizers.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList();
        ViewBag.Comparators = comparators.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();

        var assignment = await manager.GetAssignmentByIdAsync(id, user);
        if (assignment != null)
        {
          return View(new EditAssignmentModel
          {
            Title = assignment.Title,
            ComparisonServiceId = assignment.ComparisonServiceId,
            Id = assignment.Id,
            TokenizerServiceId = assignment.TokenizerServiceId,
          });
        }
      }

      return BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var success = await this.manager.DeleteAssingnmentAsync(id, user);
        if (success)
          return RedirectToAction("Index");
      }

      return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditAssignmentModel model)
    {
      // TODO: Check if existing
      // if (!ModelState.IsValid)
      // {
      // }
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var success = await this.manager.SaveAssignmentAsync(new Assignment
        {
          Id = model.Id ?? default(int),
          Title = model.Title,
          ComparisonServiceId = model.ComparisonServiceId.Value,
          TokenizerServiceId = model.TokenizerServiceId.Value
        }, user);

        if (success)
        {
          return RedirectToAction("Index");
        }
      }

      return BadRequest();
    }
  }
}
