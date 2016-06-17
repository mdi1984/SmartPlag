using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Model;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class SubmissionController : Controller
  {
    private AssignmentManager assignmentManager;
    private SubmissionManager submissionManager;

    public SubmissionController(AssignmentManager assignmentManager, SubmissionManager submissionManager)
    {
      this.assignmentManager = assignmentManager;
      this.submissionManager = submissionManager;
    }

    public async Task<IActionResult> Index(int assignmentId)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      var assignment = await this.assignmentManager.GetAssignmentByIdAsync(assignmentId, user);

      return View(new SubmissionListModel
      {
        Assignment = assignment
      });
    }

    public IActionResult Create(int assignmentId)
    {
      ViewBag.AssignmentId = assignmentId;
      return View("Edit");
    }

    [HttpPost]
    public async Task<IActionResult> Create(int assignmentId, EditSubmissionModel model, ICollection<IFormFile> files)
    {
      if (files.Count == 0)
        return new BadRequestObjectResult(new { error = "No files selected" });

      var submission = new Submission
      {
        AssignmentId = assignmentId,
        FirstName = model.FirstName,
        LastName = model.LastName
      };

      // save submission first 
      if (!await this.submissionManager.SaveSubmissionAsync(submission))
      {
        return BadRequest();
      }

      var submissionFiles = new List<StudentFile>();
      
      // add files:
      // TODO: Check for supported File Extensions 
      foreach (var file in files)
      {
        var submissionFile = new StudentFile
        {
          SubmissionId = submission.Id,
          FileName = file.FileName
        };

        // TODO: Check encoding etc. Assume UTF-8 for now
        using (var fileStream = file.OpenReadStream())
        {
          var buffer = new byte[fileStream.Length];
          fileStream.Read(buffer, 0, int.MaxValue);
          var content = System.Text.Encoding.UTF8.GetString(buffer);
          submissionFile.Content = content;
        }

        submissionFiles.Add(submissionFile);
      }

      await this.submissionManager.SaveFilesAsync(submissionFiles);

      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      return RedirectToAction("Index", new { assignmentId = assignmentId });
    }
  }
}