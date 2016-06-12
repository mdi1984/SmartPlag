using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class AssignmentManager
  {
    private PlagContextFactory contextFactory;

    public AssignmentManager(PlagContextFactory contextFactory)
    {
      this.contextFactory = contextFactory;
    }

    public IList<Assignment> GetAssignments()
    {
      using (var context = this.contextFactory.Create())
      {
        return context.Assignments
                      .Include(a => a.TokenizerService)
                      .Include(a => a.ComparisonService)
                      .ToList();
      }
    }

    public Assignment GetAssignment(int id)
    {
      using (var context = this.contextFactory.Create())
      {
        return context.Assignments
          .Include(a => a.TokenizerService)
          .Include(a => a.ComparisonService)
          .Include(a => a.Submissions)
            .ThenInclude(s => s.Files)
          .FirstOrDefault(a => a.Id == id);
      }
    }

    public bool SaveOrUpdateAssignment(Assignment assignment)
    {
      using (var context = this.contextFactory.Create())
      {
        try
        {
          if (assignment.Id == default(int))
          {
            context.Assignments.Add(assignment);
          }
          else
          {
            context.Update(assignment);
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

    public List<ComparisonService> GetComparisonServices()
    {
      using (var context = this.contextFactory.Create())
      {
        return context.ComparisonServices.ToList();
      }
    }

    public List<TokenizerService> GetTokenizerServices()
    {
      using (var context = this.contextFactory.Create())
      {
        return context.TokenizerServices.ToList();
      }
    }
  }
}
