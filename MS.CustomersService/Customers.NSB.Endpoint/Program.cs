using System;
using System.Threading.Tasks;
using Customers.Infrastructure.Mappings;
using Customers.Infrastructure.Migrations.MySQL;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NServiceBus;
using NServiceBus.NHibernate.Outbox;
using NServiceBus.Persistence;
using UpgFisi.Common.Infrastructure.NHibernate;
using Environment = System.Environment;
using NHEnvironment = NHibernate.Cfg.Environment;

namespace Customers.NSBEndpoint
{
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
            var endPointName = "Customers.NSBEndpoint";
            Console.Title = endPointName;
            var endpointConfiguration = new EndpointConfiguration(endPointName);
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_PCF_NSB_URL");
            transport.ConnectionString(rabbitmqUrl);
            string mysqlConnectionString = Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS");
            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            persistence.UseOutboxRecord<Outbox, OutboxMap>();
            var nHibernateConfig = new Configuration();
            nHibernateConfig.SetProperty(NHEnvironment.ConnectionProvider, typeof(NHibernate.Connection.DriverConnectionProvider).FullName);
            nHibernateConfig.SetProperty(NHEnvironment.ConnectionDriver, typeof(NHibernate.Driver.MySqlDataDriver).FullName);
            nHibernateConfig.SetProperty(NHEnvironment.Dialect, typeof(NHibernate.Dialect.MySQLDialect).FullName);
            nHibernateConfig.SetProperty(NHEnvironment.ConnectionString, mysqlConnectionString);
            AddFluentMappings(nHibernateConfig, mysqlConnectionString);
            persistence.UseConfiguration(nHibernateConfig);
            persistence.DisableSchemaUpdate();
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static Configuration AddFluentMappings(Configuration nhConfiguration, string connectionString)
        {
            return Fluently
                .Configure(nhConfiguration)
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(cfg =>
                {
                    cfg.FluentMappings.AddFromAssembly(typeof(CustomerMap).Assembly);
                    cfg.FluentMappings.Conventions.Add(
                        ForeignKey.EndsWith("_id"),
                        ConventionBuilder.Property.When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()));
                    cfg.FluentMappings.Conventions.Add<OtherConversions>();
                    cfg.FluentMappings.Conventions.Add<TableNameConvention>();
                })
                .BuildConfiguration();
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
    }
}