using System;
using Transactions.Application;
using Transactions.Messages.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;


namespace Transactions.API
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
            services.AddScoped<ITransactionApplicationService, TransactionApplicationService>();
            ConfigureEndpoint(services);
        }

        private void ConfigureEndpoint(IServiceCollection services)
        {
            var endPointName = "Transactions.API";
            var endpointConfiguration = new EndpointConfiguration(endPointName);
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_PCF_NSB_URL");
            transport.ConnectionString(rabbitmqUrl);
            endpointConfiguration.SendOnly();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(RequestMoneyTransferCommand), "Transactions.NSBEndpoint");
            routing.RouteToEndpoint(typeof(RequestMoneyDepositCommand), "Transactions.NSBEndpoint");
            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(endpoint);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }
}
