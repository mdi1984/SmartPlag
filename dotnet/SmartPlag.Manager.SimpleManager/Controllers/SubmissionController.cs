using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Manager.SimpleManager.Infrastructure;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class SubmissionController : Controller
  {
    private SubmissionManager manager;

    public SubmissionController(SubmissionManager manager)
    {
      this.manager = manager;
    }

    public IActionResult Index()
    {
      return BadRequest();
    }

    public IActionResult New(int assignmentId)
    {
      return View("NewSubmission", new Submission
      {
        AssignmentId = assignmentId
      });
    }

    [HttpPost]
    public IActionResult New(Submission submission, ICollection<IFormFile> files)
    {
      // TODO: Validate submission
      if (!this.manager.SaveOrUpdateSubmission(submission))
      {
        return BadRequest();
      }

      foreach (var file in files)
      {
        var submissionFile = new SubmissionFile
        {
          SubmissionId = submission.Id,
          FileName = file.Name
        };

        // TODO: Check encoding etc. Assume UTF-8 for now
        using (var fileStream = file.OpenReadStream())
        {
          var buffer = new byte[fileStream.Length];
          fileStream.Read(buffer, 0, int.MaxValue);
          var content = System.Text.Encoding.UTF8.GetString(buffer);
          submissionFile.Content = content;
        }

        manager.SaveFile(submissionFile);
      }

      return new OkObjectResult("heyho");
    }
  }
}
