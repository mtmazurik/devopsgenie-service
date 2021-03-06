using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DevopsGenie.Service.Tenant;
using DevopsGenie.Service.Common;
using System.Net.Http;
using devopsgenie.service.Config;
using devopsgenie.service.Common;

namespace DevopsGenie.Service
{
    public class Startup
    {
        private ILoggerFactory _loggerFactory;                                                          // use built in ASPNetCore logging 
        private ILogger<Startup> _logger;

        public Startup()  //ctor
        {

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .WithMethods("Get", "Post", "Put")
                    .AllowAnyHeader());
            });

            services.AddControllers().AddNewtonsoftJson(
                OptionsBuilderConfigurationExtensions =>
                OptionsBuilderConfigurationExtensions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // injectables
            services.AddTransient<HttpClient>();
            services.AddTransient<ITenantConfigService, TenantConfigService>();
            services.AddTransient<IJsonConfiguration, JsonConfiguration>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IEncryption, Encryption>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
