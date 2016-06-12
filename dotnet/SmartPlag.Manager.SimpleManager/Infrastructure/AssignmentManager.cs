using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        return context.Assignments.ToList();
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
  }
}
