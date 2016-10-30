using System.Configuration;
using System.Web;
using BloomService.Migrations.Migrations;
using BloomService.Web.Infrastructure;
using MongoMigrations;

[assembly: PreApplicationStartMethod(typeof(BloomService.Web.MongoMigrations), "RunMigrations")]

namespace BloomService.Web
{
    public class MongoMigrations
    {
        private static readonly BloomServiceConfiguration Configuration = BloomServiceConfiguration.FromWebConfig(ConfigurationManager.AppSettings);

        public static void RunMigrations()
        {
            var runner = new MigrationRunner(Configuration.Connection, Configuration.DbName);
            runner.MigrationLocator.LookForMigrationsInAssemblyOfType<MigrationInitial>();
            runner.UpdateToLatest();
        }
    }
}