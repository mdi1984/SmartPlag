using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class ServiceConsumer
  {
    private readonly SubmissionManager submissionManager;

    public ServiceConsumer(SubmissionManager manager)
    {
      this.submissionManager = manager;
    }

    public async Task TokenizeSubmissions(int submissionId, string user, string accessToken)
    {
      var submission = await this.submissionManager.GetSubmissionByIdAsync(submissionId, user);
      var tokenizerUrl = $"{submission.Assignment.TokenizerService.BaseUrl}/api/tokenizer";
      await Tokenize(submission, tokenizerUrl, accessToken);
    }

    private async Task Tokenize(Submission submission, string tokenizerUrl, string accessToken)
    {
      var client = new HttpClient();
      client.BaseAddress = new Uri(tokenizerUrl);
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer",
        accessToken);

      var payload = new TokenizerRequest
      {
        Title = submission.Assignment.Title,
        Assignments = new List<Model.Assignment>()
      };

      var restAssignment = new Model.Assignment
      {
        FirstName = submission.FirstName,
        LastName = submission.LastName,
        Files = new List<File>()
      };

      foreach (var file in submission.Files.Where(f => string.IsNullOrEmpty(f.TokenizedContent)))
      {
        restAssignment.Files.Add(new File
        {
          FileName = file.FileName,
          Base64Source = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(file.Content))
        });
      }

      payload.Assignments.Add(restAssignment);

      var response = await client.PostAsJsonAsync("/api/tokenizer", payload);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        var studResult = (await response.Content.ReadAsAsync<TokenizerResult>())
          ?.StudentResults
          ?.FirstOrDefault(s => s.FirstName.Equals(submission.FirstName) && s.LastName.Equals(submission.LastName));

        if (studResult != null)
        {
          foreach (var file in submission.Files.Where(f => string.IsNullOrEmpty(f.TokenizedContent)))
          {
            file.TokenizedContent =
              studResult.Files.FirstOrDefault(f => f.FileName.Equals(file.FileName)).Tokens.ToString();
          }
        }
      }

      await this.submissionManager.SaveFilesAsync(submission.Files.ToList());
    }
  }
}
