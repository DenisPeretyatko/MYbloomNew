using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using BloomService.Migrations.Migrations;
using MongoDB.Driver;
using MongoMigrations;

[assembly: PreApplicationStartMethod(typeof(BloomService.Web.MongoMigrations), "Start")]

namespace BloomService.Web
{
    public class MongoMigrations
    {
        protected readonly List<Assembly> Assemblies = new List<Assembly>();
        public List<MigrationFilter> MigrationFilters = new List<MigrationFilter>();
        public static MongoDatabase Database { get; set; }
        public static MigrationLocator MigrationLocator { get; set; }
        public static DatabaseMigrationStatus DatabaseStatus { get; set; }

        public static void Start()
        {
            var connection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var dbName = ConfigurationManager.AppSettings["MainDb"];
            var runner = new MigrationRunner(connection, dbName);
            runner.MigrationLocator.LookForMigrationsInAssemblyOfType<MigrationInitial>();
            Database = runner.Database;
            DatabaseStatus = runner.DatabaseStatus;
            MigrationLocator = runner.MigrationLocator;
            UpdateTo(MigrationLocator.LatestVersion(), runner);
        }

        public static void UpdateTo(MigrationVersion updateToVersion, MigrationRunner runner){
            var currentVersion = runner.DatabaseStatus.GetLastAppliedMigration();
            var migrations = MigrationLocator.GetMigrationsAfter(currentVersion)
                                             .Where(m => m.Version <= updateToVersion);

            ApplyMigrations(migrations);

        }
        protected  static void ApplyMigrations(IEnumerable<Migration> migrations)
        {
            migrations.ToList()
                      .ForEach(ApplyMigration);
        }
        protected static void ApplyMigration(Migration migration)
        {
            Console.WriteLine(new { Message = "Applying migration", migration.Version, migration.Description, DatabaseName = Database.Name });

            var appliedMigration = DatabaseStatus.StartMigration(migration);
            migration.Database = Database;
            migration.Update();

            DatabaseStatus.CompleteMigration(appliedMigration);
        }
        public virtual MigrationVersion LatestVersion()
        {
            if (!GetAllMigrations().Any())
            {
                return MigrationVersion.Default();
            }
            return GetAllMigrations()
                .Max(m => m.Version);
        }
        public virtual IEnumerable<Migration> GetAllMigrations()
        {
            return Assemblies
                .SelectMany(GetMigrationsFromAssembly)
                .OrderBy(m => m.Version);
        }
        protected virtual IEnumerable<Migration> GetMigrationsFromAssembly(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes()
                    .Where(t => typeof(Migration).IsAssignableFrom(t) && !t.IsAbstract)
                    .Select(Activator.CreateInstance)
                    .OfType<Migration>()
                    .Where(m => !MigrationFilters.Any(f => f.Exclude(m)));
            }
            catch (Exception exception)
            {
                throw new MigrationException("Cannot load migrations from assembly: " + assembly.FullName, exception);
            }
        }

    }
}