﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using ParkShark.API.Controllers.Divisions.Mappers;
using ParkShark.API.Controllers.Divisions.Mappers.Interfaces;
using ParkShark.Domain.Divisions.Repository;
using ParkShark.Services.Divisions;
using ParkShark.Services.Divisions.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace ParkShark.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public readonly ILoggerFactory efLoggerFactory
          = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category.Contains("Command") && level == LogLevel.Information, true) });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ParkShark.Api", Version = "v1" });
            });
            services.AddSingleton<IDivisionServices, DivisionServices>();
            services.AddSingleton<IDivisionMapper, DivisionMapper>();
            services.AddSingleton<ILoggerFactory>(efLoggerFactory);
            services.AddTransient<DivisionDbContext>((sp) => 
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("ParkSharkDb");
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                return new DivisionDbContext(connectionString, loggerFactory);
            });
     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
}