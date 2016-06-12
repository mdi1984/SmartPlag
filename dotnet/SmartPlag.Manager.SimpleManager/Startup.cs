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
      services.AddScoped<PlagContextFactory>();
      services.AddScoped<AssignmentManager>();
      services.AddScoped<SubmissionManager>();
      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, PlagContext plagContext)
    {
      app.UseDeveloperExceptionPage();

      plagContext.Database.EnsureCreated();
      plagContext.Database.Migrate();

      if (!plagContext.TokenizerServices.Any())
      {
        plagContext.TokenizerServices.Add(new Infrastructure.Model.TokenizerService
        {
          Name = "C# Tokenizer",
          ServiceUrl = "http://localhost:1337/api/usw"
        });
        plagContext.SaveChanges();
      }

      if (!plagContext.ComparisonServices.Any())
      {
        plagContext.ComparisonServices.Add(new Infrastructure.Model.ComparisonService
        {
          Name = "GST-Comparator",
          ServiceUrl = "http://localhost:1337/api/compare"
        });
        plagContext.SaveChanges();
      }

      app.UseStaticFiles();
      app.UseMvc(routes =>
      {
        routes.MapRoute("assignment", "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
