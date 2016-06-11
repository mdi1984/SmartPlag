using Microsoft.EntityFrameworkCore;
using OpenIddict;

namespace SmartPlag.Identity.Model
{
  public class ApplicationDbContext : OpenIddictContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
  }
}