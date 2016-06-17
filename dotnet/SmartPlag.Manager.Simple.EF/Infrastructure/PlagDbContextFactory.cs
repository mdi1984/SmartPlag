using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SmartPlag.Manager.Simple.EF.Infrastructure
{
  public class PlagDbContextFactory : IDbContextFactory<PlagContext>
  {
    public PlagContext Create()
    {
      var connectionStr =
        "Data Source=MDI-XMG\\SQLEXPRESS;Initial Catalog=SmartPlagSimpleManagerV2;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
      var optionBuilder = new DbContextOptionsBuilder<PlagContext>();
      optionBuilder.UseSqlServer(connectionStr);
      return new PlagContext(optionBuilder.Options);
    }
  }
}
