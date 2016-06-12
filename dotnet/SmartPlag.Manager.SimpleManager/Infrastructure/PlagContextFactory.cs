using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class PlagContextFactory : IDbContextFactory<PlagContext>
  {
    public PlagContext Create()
    {
      var connection = @"Data Source=MDI-XMG\SQLEXPRESS;Initial Catalog=SmartPlagSimpleManager;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
      var optionsBuilder = new DbContextOptionsBuilder<PlagContext>();
      optionsBuilder.UseSqlServer(connection);

      return new PlagContext(optionsBuilder.Options);
    }
  }
}