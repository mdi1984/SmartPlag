using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model.Request;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model.Response;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Controllers
{
  [Route("api/[controller]")]
  public class ComparisonController
  {
    [HttpPost]
    [Authorize]
    public IActionResult SimpleComparison([FromBody] ComparisonRequest request)
    {
      try
      {
        //var matches = new Dictionary<string, IList<Match>>();
        var matches = new List<MatchResult>();
        var submissions = request.Submissions.Select(
          s => new StudentSequence()
          {
            FirstName = s.FirstName,
            LastName = s.LastName,
            TokenSequence = CreateCombinedSequence(s)
          }).ToList();

        // compare every submission with every other submission
        foreach (var curSequence in submissions)
        {
          foreach (var otherSequence in submissions)
          {
            var tmpResult = new MatchResult(
              new Student(curSequence.FirstName, curSequence.LastName),
              new Student(otherSequence.FirstName, otherSequence.LastName));

            if (curSequence == otherSequence)
              continue;

            var match = GstComparator.Compare<int>(curSequence.TokenSequence, otherSequence.TokenSequence, 9)
              .OrderBy(m => m.PatternIndex)
              .ToList();

            tmpResult.Matches = match;
            if (match != null && match.Count > 0)
            {
              matches.Add(tmpResult);
            }
            //matches.Add($"{curSequence.Name} - {otherSequence.Name}", match);
            
            curSequence.TokenSequence.Reset();
            otherSequence.TokenSequence.Reset();
          }
        }

        return new ObjectResult(matches);
      }
      catch (Exception)
      {
        return new BadRequestObjectResult("Invalid request format");
      }
    }

    private Sequence<int> CreateCombinedSequence(StudentSubmission submission)
    {
      return new Sequence<int>(
        submission.Files
        .SelectMany(f => f.Tokens.Select(t => new Token<int>(t)))
        .ToList());
    }
  }
}