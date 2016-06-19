using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Infrastructure;
using SmartPlag.Manager.SimpleManager.Model;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class SubmissionController : Controller
  {
    private AssignmentManager assignmentManager;
    private SubmissionManager submissionManager;
    private ServiceConsumer serviceConsumer;

    public SubmissionController(AssignmentManager assignmentManager, SubmissionManager submissionManager, ServiceConsumer serviceConsumer)
    {
      this.assignmentManager = assignmentManager;
      this.submissionManager = submissionManager;
      this.serviceConsumer = serviceConsumer;
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

    public async Task<IActionResult> Details(int assignmentId, int id)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      var submission = await this.submissionManager.GetSubmissionByIdAsync(id, user);

      return View(submission);
    }

    public async Task<IActionResult> DeleteFile(int id, int assignmentId, int submissionid)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      var success = await this.submissionManager.DeleteFileAsync(id, user);
      if (success)
      {
        return RedirectToAction("Details", new { assignmentId = assignmentId, id = submissionid });
      }

      return BadRequest();
    }

    public async Task<IActionResult> NewFile(int assignmentId, int id, ICollection<IFormFile> files)
    {
      if (files.Count == 0)
        return new BadRequestObjectResult(new { error = "No files selected" });

      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      // TODO: Validate user

      var submissionFiles = new List<StudentFile>();

      // add files:
      // TODO: Check for supported File Extensions 
      foreach (var file in files)
      {
        var submissionFile = new StudentFile
        {
          SubmissionId = id,
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

      var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
      Task.Run(() => this.serviceConsumer.TokenizeSubmissions(id, user, accessToken));

      return RedirectToAction("Details", new { assignmentId = assignmentId, id = id });
    }

    [HttpPost]
    public async Task<IActionResult> Create(int assignmentId, EditSubmissionModel model, ICollection<IFormFile> files)
    {
      if (files.Count == 0)
        return new BadRequestObjectResult(new { error = "No files selected" });

      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      // TODO: Validate user

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

      var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
      Task.Run(() => this.serviceConsumer.TokenizeSubmissions(submission.Id, user, accessToken));
      return RedirectToAction("Index", new { assignmentId = assignmentId });
    }
  }
}