using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class SubmissionManager
  {
    private PlagContextFactory contextFactory;

    public SubmissionManager(PlagContextFactory contextFactory)
    {
      this.contextFactory = contextFactory;
    }

    public bool SaveOrUpdateSubmission(Submission submission)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {

          if (submission.Id == default(int))
          {
            context.Submissions.Add(submission);
          }
          else
          {
            context.Update(submission);
          }

          context.SaveChanges();
          return true;
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

    public bool SaveFile(SubmissionFile submissionFile)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          context.SubmissionFiles.Add(submissionFile);
          context.SaveChanges();
          return true;
        }
        catch (Exception)
        {
          return false; 
        }
      }
    }
  }
}