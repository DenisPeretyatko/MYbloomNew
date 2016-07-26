using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoMigrations;

namespace BloomService.Migrations.Migrations
{
    public class MigrationAddMoreMemory_20160726160929 : Migration
    {
        public MigrationAddMoreMemory_20160726160929() : base("0.0.6")
        {

        }

        public override void Update()
        {
             Database.Eval(new BsonJavaScript("db.adminCommand({setParameter: 1, internalQueryExecMaxBlockingSortBytes: 134217728});"));
        }
    }
}