﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.Simple.EF.Infrastructure;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.Simple.EF
{
  public class SubmissionManager
  {
    private PlagDbContextFactory contextFactory;

    public SubmissionManager(PlagDbContextFactory contextFactory)
    {
      this.contextFactory = contextFactory;
    }

    public async Task<bool> SaveSubmissionAsync(Submission submission)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          var curFiles = submission.Files;
          submission.Files = null;

          if (submission.Id == default(int))
          {
            context.Submissions.Add(submission);
          }
          else
          {
            var dbSubmission = context.Submissions.FirstOrDefault(s => s.Id == submission.Id);

            if (dbSubmission == null)
            {
              return false;
            }

            // CurrentValues.SetValues not implemented yet - do it manually for now -_-
            // https://github.com/aspnet/EntityFramework/issues/1200
            dbSubmission.FirstName = submission.FirstName;
            dbSubmission.LastName = submission.LastName;
            dbSubmission.Files = null;

            context.Submissions.Update(dbSubmission);
          }

          await context.SaveChangesAsync();
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }
    }

    public async Task<bool> SaveFilesAsync(List<StudentFile> submissionFiles)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          // save new files
          var newFiles = submissionFiles.Where(f => f.Id == default(int)).ToList();
          context.Files.AddRange(submissionFiles);

          // update existing
          var existingFileIds = submissionFiles.Where(f => f.Id != default(int)).Select(f => f.Id).ToList();
          var dbExistingFiles = await context.Files.Where(f => existingFileIds.Contains(f.Id)).ToListAsync();

          foreach (var existingFile in dbExistingFiles)
          {
            var curFile = submissionFiles.First(f => f.Id == existingFile.Id);

            existingFile.Content = curFile.Content;
            existingFile.FileName = curFile.FileName;
            context.Entry(existingFile).State = EntityState.Modified;
          }

          await context.SaveChangesAsync();
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }
    }
  }
}