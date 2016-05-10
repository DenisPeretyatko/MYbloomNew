namespace BloomService.Domain.Repositories.Concrete
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Repositories.Abstract;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class EntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly MongoCollection<TEntity> collection;

        public EntityRepository(string collectionName)
        {
            var mongoDbConnection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var mongoDbName = ConfigurationManager.AppSettings["MongoDbName"];

            var client = new MongoClient(mongoDbConnection);
            var server = client.GetServer();
            var database = server.GetDatabase("local");
            collection = database.GetCollection<TEntity>(collectionName);
        }

        protected string Connection { get; set; }

        public bool Add(TEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            else if (Get(entity.Id) != null)
            {
                collection.Remove(Query.EQ("_id", entity.Id));
            }

            return collection.Insert(entity).HasLastErrorMessage;
        }

        public bool Delete(TEntity entity)
        {
            return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IQueryable<TEntity> Get()
        {
            return collection.AsQueryable();
        }

        public TEntity Get(string id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable().Where(predicate.Compile()).AsQueryable();
        }
    }
}