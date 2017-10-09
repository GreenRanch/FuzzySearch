using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FuzzySearch.Core;
using Microsoft.Extensions.Configuration;
using FuzzySearch.Core.Models.Config;
using FuzzySearch.Data;

namespace FuzzySearch.Service
{
  public class Startup
  {

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder().
        SetBasePath(env.ContentRootPath).
        AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).
        AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      var documentDbSettings = Configuration.GetSection("DocumentDb");
      services.Configure<DocumentDbConfig>(documentDbSettings);

      services.AddMvc();

      services.AddTransient<IFuzzySearchService, FuzzySearchService>();
      services.AddTransient<IAzureDocDatabase, AzureDocDatabase>();
      services.AddTransient<IFuzzySearchHelper, FuzzySearchHelper>();      
    }


    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 &&
           !Path.HasExtension(context.Request.Path.Value) &&
           !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
    }
  }
}
