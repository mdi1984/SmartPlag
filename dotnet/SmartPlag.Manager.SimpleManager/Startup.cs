﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SmartPlag.Manager.SimpleManager
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app)
    {
      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}
