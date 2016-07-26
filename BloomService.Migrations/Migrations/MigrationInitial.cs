using MongoMigrations;

namespace BloomService.Migrations.Migrations
{
    public class MigrationInitial : Migration
    {
        public MigrationInitial() : base("0.0.0")
        {

        }
        public override void Update()
        {
            //Database.Eval(new BsonJavaScript("db.WorkOrderCollection.createIndex( { WorkOrder:1, ScheduleDate: 1, DateEntered: 1, ARCustomer: 1, Location: 1, Status: 1 } );"));
        }
    }
}