using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model.Rest;
using Assignment = SmartPlag.Manager.SimpleManager.Infrastructure.Model.Assignment;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class TokenizerManager
  {
    private TokenizerService service;

    public TokenizerManager(TokenizerService service) 
    {
      this.service = service;
    }

    public async void Tokenize(Assignment assignment)
    {
      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(this.service.ServiceUrl);
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var payload = new TokenizerRequest
      {
        Title = assignment.Title,
        Assignments = new List<Model.Rest.Assignment>()
      };

      foreach (var submission in assignment.Submissions.Where(a => a.Files.Any(f => string.IsNullOrEmpty(f.TokenizedJsonContent))))
      {
        var restAssignment = new Model.Rest.Assignment
        {
          FirstName = submission.FirstName,
          LastName = submission.LastName,
          Files = new List<File>()
        };

        foreach (var file in submission.Files.Where(f => string.IsNullOrEmpty(f.TokenizedJsonContent)))
        {
          restAssignment.Files.Add(new File
          {
            FileName = file.FileName,
            Base64Source = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(file.Content))
          });
        }

        payload.Assignments.Add(restAssignment);
      }

      var response = await client.PostAsJsonAsync("/api/tokenizer", payload);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        var result = await response.Content.ReadAsAsync<TokenizerResponse>();

      }
    }
  }
}