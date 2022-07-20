using Company.API.Helpers;
using Company.Common.Connection.v1;
using Company.Common.Inerfaces;
using Company.Common.Services;
using Company.Infrastructure.Interfaces;
using Company.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Company.API
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
            // Mongo Connection
            services.Configure<DatabaseSettings>(Configuration.GetSection("ServiceDatabase"));

            // Add Automapper
            services.AddAutoMapper(typeof(Startup));

            #region Mongo Services
            // Singletons
            services.AddSingleton<UserService>();
            services.AddSingleton<DeveloperService>();
            services.AddSingleton<CompanyService>();

            // Shared services
            services.AddScoped<IGenericReturnableHelper, GenericReturnableHelper>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HeyBaldur Companybase API",
                    Version = "v1",
                    Description = "In HCAPI, company records store information about a business or organization. The Companies API allows you to manage this data and sync. Also, HCAPI is a platform for finding business information about private and public companies. It provides intelligent prospecting software powered by live company data."
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
