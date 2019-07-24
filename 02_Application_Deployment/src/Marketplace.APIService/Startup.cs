using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace Marketplace.APIService
{
    public class Startup
    {
        //Enable Reserved Proxy - Add ReservedName, the name can be found in the Service Fabric Explorer
        private const string ServiceNameUrl = "/Marketplace/Marketplace.APIService";        
        
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            //Enable Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info: new Info());
            });
            services.AddCors();
            services.AddSingleton<IServiceAgent, ServiceAgent>( s => new ServiceAgent("fabric:/Marketplace/Marketplace.BC.Quorum.API", new System.Net.Http.HttpClient()));
            services.AddSingleton<ITransactionIndexerServiceAgent, TransactionIndexerServiceAgent>(t => new TransactionIndexerServiceAgent(Configuration["MongoConnectionString"]));
            services.AddSingleton<IBusinessLogicLayer, BusinessLogicLayer>(b => new BusinessLogicLayer(Configuration["MongoConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Enable Reserved Proxy - handle transform            
            app.Use((context, next) =>
            {
                //Apply the fabric URL if accessing by the Service Fabric Reverse Proxy
                //This URL will be use to rendering the resource URL. So all reference resource should be start with ~/
                if (context.Request.Headers.TryGetValue("X-Forwarded-Host", out var _))
                    context.Request.PathBase = $"{ServiceNameUrl}/";

                return next();
            });

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quorum Client Services");
            });

            app.UseStaticFiles();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
