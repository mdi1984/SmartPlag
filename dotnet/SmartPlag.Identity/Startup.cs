using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict;
using SmartPlag.Identity.Model;

namespace SmartPlag.Identity
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

      // TODO: Check if we need this
      services.AddMvc();

      // add entity framework using the config connection string
      services.AddEntityFrameworkSqlServer()
        .AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]));


      // add identity
      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      // add OpenIddict
      services.AddOpenIddict<ApplicationUser, IdentityRole, ApplicationDbContext>()
          .DisableHttpsRequirement()
          .UseJsonWebTokens();
    }

    public async void Configure(IApplicationBuilder app, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      app.UseDeveloperExceptionPage();
      app.UseOpenIddict();

      using (var context = new ApplicationDbContext(
        app.ApplicationServices.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
      {
        context.Database.EnsureCreated();
        if (!context.Applications.Any())
        {
          context.Applications.Add(new OpenIddictApplication
          {
            Id = "smartplag-simplemanager",
            DisplayName = "SmartPlag SimpleManager",
            RedirectUri = string.Empty,
            LogoutRedirectUri = string.Empty,
            Secret = Crypto.HashPassword("super_secret_manager_password"),
            Type = OpenIddictConstants.ClientTypes.Confidential
          });
        }

        context.SaveChanges();
      }

      // use jwt bearer authentication
      app.UseJwtBearerAuthentication(new JwtBearerOptions
      {
        AutomaticAuthenticate = true,
        AutomaticChallenge = true,
        RequireHttpsMetadata = false,
#if DEBUG
        Audience = "http://localhost:2406/",
        Authority = "http://localhost:2406/"
#else
        Audience = "some-azure-url",
        Authority = "some-azure-url"
#endif
      });

      var email = "mdi1984@gmail.com";
      ApplicationUser user;
      if (await userManager.FindByEmailAsync(email) == null)
      {
        // use the create rather than addorupdate so can set password
        user = new ApplicationUser
        {
          UserName = email,
          Email = email,
          EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "SuperPass");
      }

      user = await userManager.FindByEmailAsync(email);
      var roleName = "Administrator";
      if (await roleManager.FindByNameAsync(roleName) == null)
      {
        await roleManager.CreateAsync(new IdentityRole() { Name = roleName });
      }

      if (!await userManager.IsInRoleAsync(user, roleName))
      {
        await userManager.AddToRoleAsync(user, roleName);
      }
    }
  }
}
