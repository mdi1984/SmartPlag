using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartPlag.CSharp.Model;

namespace SmartPlag.CSharp.Controllers
{
  [Route("api/[controller]")]
  public class TokenizerController : Controller
  {
    [HttpGet("mappings")]
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
    public IActionResult Post([FromBody] AssignmentRequest request)
    {
      try
      {
        var response = new AssignmentResponse(request.Title);
        foreach (var assignment in request.Assignments)
        {
          var source = Encoding.UTF8.GetString(Convert.FromBase64String(assignment.Base64Source));
          var tree = CSharpSyntaxTree.ParseText(source);
          var root = (CompilationUnitSyntax)tree.GetRoot();
          var tokens = root.DescendantTokens();

          var studentResult = new StudentTokenMap(assignment.FirstName, assignment.LastName);
          foreach (var token in tokens)
          {
            studentResult.Tokens.Add(new TokenWithPosition(token.RawKind, token.FullSpan.Start, token.FullSpan.End));
          }

          response.StudentResults.Add(studentResult);
        }

        return new ObjectResult(response);

      }
      catch (Exception ex)
      {
        return new BadRequestObjectResult("Invalid request format");
      }
    }
  }
}