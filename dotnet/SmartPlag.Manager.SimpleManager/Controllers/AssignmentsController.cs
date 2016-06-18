using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Infrastructure;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;
using SmartPlag.Manager.SimpleManager.Model;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  [Authorize]
  public class AssignmentsController : Controller
  {
    private AssignmentManager manager;
    private ServiceConsumer serviceConsumer;

    public AssignmentsController(AssignmentManager manager, ServiceConsumer serviceConsumer)
    {
      this.manager = manager;
      this.serviceConsumer = serviceConsumer;
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

    public async Task<IActionResult> Result(int id)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var results = await this.manager.GetDistinctResultsAsync(id, user);
        return View(results);
      }

      return BadRequest();
    }

    public async Task<IActionResult> Matches(int id, int first, int second)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var result = await this.manager.GetDetailedResultAsync(id, user, first, second);
        if (result != null)
        {
          var firstContent = string.Join("", result.First.Files.Select(c => c.Content));
          var secondContent = string.Join("", result.Second.Files.Select(c => c.Content));
          var model = new AssignmentResultModel
          {
            FirstName = $"{result.First.FirstName} {result.First.LastName}",
            SecondName = $"{result.Second.FirstName} {result.Second.LastName}",
            FirstSource = firstContent,
            SecondSource = secondContent,
          };

          // map filenames and tokens. not used for now..
          // var firstTokens = new Dictionary<string, List<TokenResult>>();
          // foreach (var fFile in result.First.Files)
          // {
          //   var tokens = JsonConvert.DeserializeObject<List<TokenResult>>(fFile.TokenizedContent);
          //   firstTokens.Add(fFile.FileName, tokens);
          // }
          // 
          // var secondTokens = new Dictionary<string, List<TokenResult>>();
          // foreach (var sFile in result.Second.Files)
          // {
          //   var tokens = JsonConvert.DeserializeObject<List<TokenResult>>(sFile.TokenizedContent);
          //   firstTokens.Add(sFile.FileName, tokens);
          // }
          var firstTokens = new List<TokenResult>();
          var offset = 0;
          foreach (var fFile in result.First.Files)
          {
            var tokens = JsonConvert.DeserializeObject<List<TokenResult>>(fFile.TokenizedContent)
              .Select(t => new TokenResult
              {
                From = t.From + offset,
                To = t.To + offset,
                Token = t.Token
              })
              .ToList();

            offset += fFile.Content.Length;
            firstTokens.AddRange(tokens);
          }

          var secondTokens = new List<TokenResult>();
          foreach (var sFile in result.Second.Files)
          {
            var tokens = JsonConvert.DeserializeObject<List<TokenResult>>(sFile.TokenizedContent);
            secondTokens.AddRange(tokens);
          }

          model.MatchPositions = new List<MatchPosition>();
          foreach (var match in result.Matches)
          {
            // convert token positions/lengths to text / pattern positions/lengths

            var tStart = firstTokens.ElementAt(match.TextIndex).From;
            var pStart = secondTokens.ElementAt(match.TextIndex).From;
            var tEnd = firstTokens.ElementAt(match.TokenLength-1).To; // TODO: Rename TokenLength to TokenEnd
            model.MatchPositions.Add(new MatchPosition
            {
              TextStart = tStart,
              PatternStart = pStart,
              Length = tEnd - tStart
            });
          }

          //// apply coloring now
          //var random = new Random();
          //model.MatchPositions.Reverse();
          //foreach (var matchPos in model.MatchPositions)
          //{
          //  var color = String.Format("#{0:X6}", random.Next(0x1000000));
          //  var spanStart = $"<span style=\"color: {color};\">";

          //  model.FirstSource = model.FirstSource.Insert(matchPos.Length, "</span>");
          //  model.FirstSource = model.FirstSource.Insert(matchPos.PatternStart, spanStart);
          //}

          return View(model);
        }
      }

      return BadRequest();
    }

    public async Task<IActionResult> Evaluate(int id)
    {
      var user = User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
      if (user != null)
      {
        var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
        await this.manager.SetEvaluationStateAsync(id, user, AssignmentState.Evaluating);
        Task.Run(() => this.serviceConsumer.EvaluateAssignment(id, user, accessToken));
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
        var success = await this.manager.SaveAssignmentAsync(new Simple.EF.Model.Assignment
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
