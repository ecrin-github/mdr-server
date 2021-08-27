using System;
using System.Net;
using mdr_server.Extensions;
using mdr_server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace mdr_server
{
    public class Startup
    {
        private static IConfiguration _config;
        
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            
            // Setting for the release build for server
            /*services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("51.210.99.18"));
            });*/
            
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MDR API Documentation", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // Setting for the release build for server
            /*app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });*/

            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsProduction() || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseStaticFiles();
                
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = "The MDR API | ECRIN";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MDR API Documentation - v1");
                    c.InjectStylesheet("/documentation/swagger-custom/swagger-custom-styles.css");
                    c.InjectJavascript("/documentation/swagger-custom/swagger-custom-script.js", "text/javascript");
                    c.RoutePrefix = "documentation";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}