using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPlag.Manager.Simple.EF.Infrastructure;

namespace SmartPlag.Manager.Simple.EF
{
  public class AssignmentManager
  {
    private PlagDbContextFactory contextFactory;

    public AssignmentManager(PlagDbContextFactory contextFactory)
    {
      this.contextFactory = contextFactory;
    }
  }
}
