using MongoDB.Bson;
using MongoMigrations;

namespace BloomService.Migrations.Migrations
{
    public class MigrationAddMoreMemory : Migration
    {
        public MigrationAddMoreMemory() : base("0.0.1")
        {

        }

        public override void Update()
        {
             Database.Eval(new BsonJavaScript("db.adminCommand({setParameter: 1, internalQueryExecMaxBlockingSortBytes: 134217728});"));
        }
    }
}