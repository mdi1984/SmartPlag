using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.Simple.EF.Infrastructure;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.Simple.EF
{
  public class AssignmentManager
  {
    private PlagDbContextFactory contextFactory;

    public AssignmentManager(PlagDbContextFactory contextFactory)
    {
      this.contextFactory = contextFactory;
    }

    public async Task<IEnumerable<TokenizerService>> GetTokenizerServicesAsync()
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.TokenizerServices.ToListAsync();
      }
    }

    public async Task<IEnumerable<ComparisonService>> GetComparisonServicesAsync()
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.ComparisonServices.ToListAsync();
      }
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(string user)
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.Assignments
                            .Include(a => a.Submissions)
                            .Include(a => a.ComparisonService)
                            .Include(a => a.TokenizerService)
                            .Where(a => a.Owner.Equals(user)).ToListAsync();
      }
    }

    public async Task<Assignment> GetAssignmentByIdAsync(int id, string user)
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.Assignments
                    .Include(a => a.Submissions)
                    .Include(a => a.ComparisonService)
                    .Include(a => a.TokenizerService)
                    .FirstOrDefaultAsync(a => a.Id == id && a.Owner.Equals(user));
      }
    }

    public async Task<bool> SaveAssignmentAsync(Assignment assignment, string user)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          assignment.Owner = user;

          if (assignment.Id == default(int))
          {
            context.Assignments.Add(assignment);
          }
          else
          {
            var dbAssignment = context.Assignments.FirstOrDefault(a => a.Id == assignment.Id && a.Owner.Equals(user));
            if (dbAssignment == null)
            {
              throw new SecurityException();
            }

            // CurrentValues.SetValues not implemented yet - do it manually for now -_-
            // https://github.com/aspnet/EntityFramework/issues/1200
            dbAssignment.Title = assignment.Title;
            dbAssignment.TokenizerServiceId = assignment.TokenizerServiceId;
            dbAssignment.ComparisonServiceId = assignment.ComparisonServiceId;

            context.Assignments.Update(dbAssignment);
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

    public async Task<bool> DeleteAssingnmentAsync(int id, string owner)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {

          var dbAssignment = await context.Assignments.FirstOrDefaultAsync(a => a.Id == id && a.Owner.Equals(owner));
          if (dbAssignment != null)
          {
            context.Assignments.Remove(dbAssignment);
            await context.SaveChangesAsync();
            return true;
          }
        }
        catch (Exception ex) { }

        return false;
      }
    }

    public async Task<List<Result>> GetDistinctResultsAsync(int id, string user)
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.Results.Where(r => r.AssignmentId == id)
                  .Include(r => r.Assignment)
                  .Select(r => new Result
                  {
                    Assignment = r.Assignment,
                    First = r.First,
                    Second = r.Second,
                    MatchCount = r.Matches.Count
                  })
                  .OrderByDescending(r => r.MatchCount)
                  .Distinct()
                  .ToListAsync();
      }
    }
    
    public async Task<Result> GetDetailedResultAsync(int id, string user, int first, int second)
    {
      using (var context = this.contextFactory.Create())
      {
        return await context.Results
                       .Include(r => r.First.Files)
                       .Include(r => r.Second.Files)
                       .Include(r => r.Matches)
                       .Where(r => r.FirstId == first && r.SecondId == second && r.AssignmentId == id)
                       .FirstOrDefaultAsync();
      }
    }

    public async Task<bool> SetEvaluationStateAsync(int id, string owner, AssignmentState state)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          var dbAssignment = await context.Assignments.FirstOrDefaultAsync(a => a.Id == id && a.Owner.Equals(owner));
          if (dbAssignment != null)
          {
            dbAssignment.State = state;
            context.Entry(dbAssignment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
          }
        }
        catch (Exception ex) { }

        return false;
      }
    }

    public async Task<bool> SaveComparisonResultsAsync(int id, string owner, List<Result> results)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          var dbAssignment = await context.Assignments.FirstOrDefaultAsync(a => a.Id == id && a.Owner.Equals(owner));
          if (dbAssignment != null)
          {
            foreach (var result in results)
            {
              context.Results.Add(result);
            }

            await context.SaveChangesAsync();
            return true;
          }
        }
        catch (Exception) { }
        return false;
      }

    }
  }
}
