using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartPlag.Manager.Simple.EF;
using SmartPlag.Manager.Simple.EF.Infrastructure;

namespace SmartPlag.Manager.SimpleManager
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddJsonFile("config.json")
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddDbContext<PlagContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
      services.AddScoped<PlagDbContextFactory>();
      services.AddScoped<AssignmentManager>();
      services.AddScoped<SubmissionManager>();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, PlagContext dbContext)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      // use this for pure api stuff
      //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
      //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
      //{
      //  Authority = "http://localhost:5000",
      //  RequireHttpsMetadata = false,
      //  ScopeName = "manager",
      //  AdditionalScopes = new []
      //  {
      //    "fullaccess"
      //  },
      //  AutomaticAuthenticate = true
      //});

      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationScheme = "Cookies",
        AutomaticAuthenticate = true
      });

      var oidcOptions = new OpenIdConnectOptions
      {
        AuthenticationScheme = "oidc",
        SignInScheme = "Cookies",

        Authority = "http://localhost:5000",
        RequireHttpsMetadata = false,
        PostLogoutRedirectUri = "http://localhost:5003/",
        ClientId = "spmanager",
        ClientSecret = "managerSecret",
        ResponseType = "code id_token",
        GetClaimsFromUserInfoEndpoint = true,
        SaveTokens = true
      };

      oidcOptions.Scope.Clear();
      oidcOptions.Scope.Add("openid");
      oidcOptions.Scope.Add("profile");
      oidcOptions.Scope.Add("fullaccess");
      oidcOptions.Scope.Add("manager");
      oidcOptions.Scope.Add("tokenizer");

      app.UseOpenIdConnectAuthentication(oidcOptions);

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
                  name: "assignmentRoute",
                  template: "Assignments/{assignmentId?}/{controller=Submission}/{action=Index}");
      });

      dbContext.Database.EnsureCreated();
      //dbContext.Database.Migrate();
    }
  }
}
