using System.Configuration;
using System.Web;
using BloomService.Migrations.Migrations;
using MongoMigrations;

[assembly: PreApplicationStartMethod(typeof(BloomService.Web.MongoMigrations), "Start")]

namespace BloomService.Web
{
    public class MongoMigrations
    {
        public static void Start()
        {
            var connection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var dbName = ConfigurationManager.AppSettings["MainDb"];
            var runner = new MigrationRunner(connection, dbName); 
            runner.MigrationLocator.LookForMigrationsInAssemblyOfType<MigrationInitial>();
            runner.DatabaseStatus.ThrowIfNotLatestVersion();
            runner.MigrationLocator.GetAllMigrations();
            runner.UpdateToLatest();
        }
    }
}