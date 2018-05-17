﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure.DbManager;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelApi
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
            services.AddMvc();
            var appConfigurations = Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();
            services.AddSingleton(appConfigurations);
            services.AddTransient<IRepository<HotelRegion>, HotelRegionRepository>();
            services.AddTransient<IRepository<Hotel.Domain.Hotel>, HotelRepository>();
            services.AddTransient(x => 
                new HotelContextFactory().CreateDbContext());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDirectoryBrowser();
            app.UseMvc();
        }
    }
}
