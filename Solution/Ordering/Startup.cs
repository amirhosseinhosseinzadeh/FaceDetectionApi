using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MassTransit;
using Messaging.InterfacesConstants.Constants;
using GreenPipes;
using OrdersApi.Messages.Consumers;
using OrdersApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OrdersApi.Persistence;

namespace OrdersApi
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
            services.AddMassTransit();
            services.AddSingleton(options => Bus.Factory.CreateUsingRabbitMq(
             config =>
             {
                 config.Host("localhost", "/", c => { });
                 config.ReceiveEndpoint(RabbitMqMassTransitConstants.RegisterOrderCommandQueue, e =>
                 {
                     e.PrefetchCount = 16;
                     e.UseMessageRetry(x => x.Interval(3, TimeSpan.FromMinutes(2)));
                     e.Consumer<RegisterOrderCommandConsumer>(options);
                 });
                    // I will definietly get error here :)
                    config.ConfigureEndpoints(options);
             }));
            services.AddCors(policy =>
            {
                policy.AddPolicy("corsPolicy",
                    builder => builder
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials());
            });
            services.AddSingleton<IHostedService, BusService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrdersApi", Version = "v1" });
            });
            services.AddDbContext<OrdersDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OrderDataContext"))
            );
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrdersApi v1"));
            }
            app.UseCors("openPolicy");
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
