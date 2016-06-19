using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Model;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;
using Assignment = SmartPlag.Manager.Simple.EF.Model.Assignment;
using Match = SmartPlag.Manager.Simple.EF.Model.Match;
using Submission = SmartPlag.Manager.Simple.EF.Model.Submission;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class ServiceConsumer
  {
    private readonly AssignmentManager assignmentManager;
    private readonly SubmissionManager submissionManager;

    public ServiceConsumer(SubmissionManager submissionManager, AssignmentManager assignmentManager)
    {
      this.submissionManager = submissionManager;
      this.assignmentManager = assignmentManager;
    }

    public async Task EvaluateAssignment(int assignmentId, string user, string accessToken)
    {
      // get full assignment
      var assignment = await this.assignmentManager.GetAssignmentByIdAsync(assignmentId, user);

      foreach (var submission in assignment.Submissions)
      {
        await this.TokenizeSubmissions(submission.Id, user, accessToken);
      }

      // call comparison service and store results
      await this.CompareSubmissions(assignment, user, assignment.ComparisonService.BaseUrl, accessToken);


      // set evaluationstate to false
    }

    private async Task CompareSubmissions(Assignment assignment, string user, string comparatorUrl, string accessToken)
    {
      var client = new HttpClient();
      client.BaseAddress = new Uri(comparatorUrl);
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer",
        accessToken);

      var payload = new Model.ComparisonRequest
      {
        Title = assignment.Title,
        Submissions = new List<Model.Submission>()
      };

      foreach (var submission in assignment.Submissions)
      {
        var curSubmission = await this.submissionManager.GetSubmissionByIdAsync(submission.Id, user);
        var reqSubmission = new Model.Submission
        {
          FirstName = submission.FirstName,
          LastName = submission.LastName,
          Files = new List<TokenizedFile>()
        };

        foreach (var file in curSubmission.Files)
        {
          var reqFile = new TokenizedFile
          {
            FileName = file.FileName,
            Tokens = new List<int>()
          };

          var tokenList = JsonConvert.DeserializeObject<List<TokenWithPosition>>(file.TokenizedContent);
          reqFile.Tokens = tokenList.Select(t => t.Token).ToList();

          reqSubmission.Files.Add(reqFile);
        }

        payload.Submissions.Add(reqSubmission);
      }

      var response = await client.PostAsJsonAsync("/api/comparison", payload);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        var comparatorResults = await response.Content.ReadAsAsync<List<ComparatorResult>>();
        var entityResults = new List<Result>();
        foreach (var comparatorResult in comparatorResults)
        {
          var firstSubmission =
            assignment.Submissions.FirstOrDefault(
              s =>
                s.FirstName.Equals(comparatorResult.First.FirstName) &&
                s.LastName.Equals(comparatorResult.First.LastName));

          var secondSubmission =
            assignment.Submissions.FirstOrDefault(
              s =>
                s.FirstName.Equals(comparatorResult.Second.FirstName) &&
                s.LastName.Equals(comparatorResult.Second.LastName));

          var entityResult = new Result
          {
            AssignmentId = assignment.Id,
            FirstId = firstSubmission.Id,
            SecondId = secondSubmission.Id,
            Matches = new List<Match>()
          };

          foreach (var match in comparatorResult.Matches)
          {
            entityResult.Matches.Add(new Match
            {
              Result = entityResult,
              PatternIndex = match.PatternIndex,
              TextIndex = match.TextIndex,
              TokenLength = match.Length
            });
          }

          entityResults.Add(entityResult);
        }

        await this.assignmentManager.SaveComparisonResultsAsync(assignment.Id, user, entityResults);
      }

      await this.assignmentManager.SetEvaluationStateAsync(assignment.Id, user, AssignmentState.Evaluated);
    }

    public async Task TokenizeSubmissions(int submissionId, string user, string accessToken)
    {
      var submission = await this.submissionManager.GetSubmissionByIdAsync(submissionId, user);
      var tokenizerUrl = $"{submission.Assignment.TokenizerService.BaseUrl}/api/tokenizer";
      await this.Tokenize(submission, tokenizerUrl, accessToken);
    }

    private async Task Tokenize(Submission submission, string tokenizerUrl, string accessToken)
    {
      // check if all files are tokenized - if so, abort
      var fullyTokenized = !submission.Files.Any(f => string.IsNullOrEmpty(f.TokenizedContent) && !string.IsNullOrEmpty(f.Content));
      if (fullyTokenized)
        return;

      var client = new HttpClient();
      client.BaseAddress = new Uri(tokenizerUrl);
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer",
        accessToken);

      var payload = new Model.TokenizerRequest
      {
        Title = submission.Assignment.Title,
        Assignments = new List<Model.Assignment>()
      };

      var restAssignment = new Model.Assignment
      {
        FirstName = submission.FirstName,
        LastName = submission.LastName,
        Files = new List<Model.File>()
      };

      foreach (var file in submission.Files.Where(f => string.IsNullOrEmpty(f.TokenizedContent)))
      {
        restAssignment.Files.Add(new Model.File
        {
          FileName = file.FileName,
          Base64Source = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(file.Content))
        });
      }

      payload.Assignments.Add(restAssignment);

      var response = await client.PostAsJsonAsync("/api/tokenizer", payload);
      if (response.StatusCode == System.Net.HttpStatusCode.OK)
      {
        var studResult = (await response.Content.ReadAsAsync<Model.TokenizerResult>())
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
