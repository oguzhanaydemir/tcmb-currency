using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Protein.Tech.TCMB.Core.Application;
using Protein.Tech.TCMB.Infrastructure.Persistence;
using Protein.Tech.TCMB.Infrastructure.Persistence.Services;
using System;

namespace Protein.Tech.TCMB.Api
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
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("Hangfire")));
            services.AddHangfireServer();
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Protein.Tech.TCMB.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Protein.Tech.TCMB.Api v1"));
            }
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                DashboardTitle = "Hangfire Dashboard",
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //recurringJobManager.AddOrUpdate("currency-today", () => serviceProvider.GetService<ITodayJob>().RunAsync(), "* * * * *");
            recurringJobManager.AddOrUpdate<ITodayJob>("currency-today", (today) => today.RunAsync(), "30 15 * * *");
        }
    }
}
