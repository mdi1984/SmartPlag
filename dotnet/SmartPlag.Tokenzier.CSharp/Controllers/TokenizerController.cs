using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartPlag.Tokenzier.CSharp.Model;

namespace SmartPlag.Tokenzier.CSharp.Controllers
{
  [Route("api/[controller]")]
  public class TokenizerController : Controller
  {
    [HttpGet("mappings")]
    [Authorize]
    public IActionResult Mappings()
    {
      var result = new List<Token>();
      foreach (var kind in Enum.GetValues(typeof(SyntaxKind)))
      {
        result.Add(new Token(kind.ToString(), kind.GetHashCode()));
      }

      return new ObjectResult(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] AssignmentRequest request)
    {
      try
      {
        var response = new AssignmentResponse(request.Title);
        foreach (var assignment in request.Assignments)
        {
          var studentResult = new StudentResult(assignment.FirstName, assignment.LastName);
          response.StudentResults.Add(studentResult);

          if (assignment.Files == null)
            continue;

          foreach (var file in assignment.Files)
          {
            var source = Encoding.UTF8.GetString(Convert.FromBase64String(file.Base64Source));
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var tokens = root.DescendantTokens();

            var tokenMap = new StudentTokenMap(file.FileName);
            studentResult.Files.Add(tokenMap);

            foreach (var token in tokens)
            {
              tokenMap.Tokens.Add(new TokenWithPosition(token.RawKind, token.FullSpan.Start, token.FullSpan.End));
            }

          }
        }

        return new ObjectResult(response);

      }
      catch (Exception)
      {
        return new BadRequestObjectResult("Invalid request format");
      }
    }
  }
}