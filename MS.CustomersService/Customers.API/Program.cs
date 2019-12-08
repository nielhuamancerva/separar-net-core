using System;
using Customers.Infrastructure.Migrations.MySQL;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }*/
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IServiceProvider CreateServices()
        {
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS");
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .WithGlobalCommandTimeout(new TimeSpan(1, 0, 0))
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(CreateInitialSchema).Assembly)
                    .For.All()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
