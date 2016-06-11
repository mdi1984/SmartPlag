using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartPlag.Manager.SimpleManager.Infrastructure;

namespace SmartPlag.Manager.SimpleManager
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      var connection = @"Data Source=MDI-XMG\SQLEXPRESS;Initial Catalog=SmartPlagSimpleManager;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
      services.AddDbContext<PlagContext>(options => options.UseSqlServer(connection));

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, PlagContext plagContext)
    {
      plagContext.Database.EnsureCreated();

      app.UseStaticFiles();
      app.UseMvc(routes =>
      {
        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
