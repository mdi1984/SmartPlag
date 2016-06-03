using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model;
using SmartPlag.Comparison.Service.GreedyStringTiling.Model.Request;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Controllers
{
  [Route("api/[controller]")]
  public class ComparisonController
  {
    [HttpPost]
    public IActionResult SimpleComparison([FromBody] ComparisonRequest request)
    {
      try
      {
        var matches = new Dictionary<string, IList<Match>>();
        var submissions = request.Submissions.Select(
          s => new StudentSequence()
          {
            Name = s.Name,
            TokenSequence = CreateCombinedSequence(s)
          }).ToList();

        // compare every submission with every other submission
        foreach (var curSequence in submissions)
        {
          foreach (var otherSequence in submissions)
          {
            if (curSequence == otherSequence)
              continue;

            var match = GstComparator.Compare<int>(curSequence.TokenSequence, otherSequence.TokenSequence, 3)
              .OrderBy(m => m.PatternIndex)
              .ToList();

            matches.Add($"{curSequence.Name} - {otherSequence.Name}", match);
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