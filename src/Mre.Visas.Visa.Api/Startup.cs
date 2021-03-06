using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Mre.Sb.Auditar;
using Mre.Visas.Visa.Api.Extensions;
using Mre.Visas.Visa.Application;
using Mre.Visas.Visa.Infrastructure;
using Mre.Visas.Visa.Infrastructure.Persistence.Contexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructureLayer(Configuration);
      services.AddApplicationLayer();

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });

      //services.AddSwaggerGen(c =>
      //{
      //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mre.Visas.Visa.Api", Version = "v1" });
      //});

      services.AddSwaggerExtension("Mre.Visas.Visa.Api", "v1");
      services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder
          .WithOrigins
          (
            Configuration["App:CorsOrigins"]
              .Split(",", StringSplitOptions.RemoveEmptyEntries)
              .ToArray()
          )
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
        });
      });

      services.AddMvc();
            

      services.AgregarAuditoria(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        //app.UseSwagger();
        //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mre.Visas.Visa.Api v1"));
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseSwaggerExtension("Mre.Visas.Visa.Api");
      app.UseApiExceptionMiddleware();

      app.UseAuthorization();
      //ADD CROSS
      // global cors policy
      app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials()); // allow credentials

      app.UseEndpoints(endpoints => endpoints.MapControllers());
            
      app.UsarAuditoria<ApplicationDbContext>();
    }
  }
}
