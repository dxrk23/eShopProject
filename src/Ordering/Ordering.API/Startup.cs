using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ordering.API.Extentions;
using Ordering.API.RabbitMQ;
using Ordering.Core.Repositories;
using Ordering.Core.Repositories.Base;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Repositories.Base;
using RabbitMQ.Client;

namespace Ordering.API
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
            services.AddControllers();
            services.AddDbContext<OrderContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IRabbitMQConnection>(s =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                factory.UserName = Configuration["EventBus:Username"];
                factory.Password = Configuration["EventBus:Password"];

                return new RabbitMQConnection(factory);
            });
            services.AddSingleton<EventBusRabbitMQConsumer>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Ordering API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1"); });
        }
    }
}