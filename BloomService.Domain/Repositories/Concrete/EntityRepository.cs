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
        protected readonly MongoCollection<TEntity> Collection;

        public EntityRepository(string collectionName)
        {
            var mongoDbConnection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var mongoDbName = ConfigurationManager.AppSettings["MongoDbName"];

            var client = new MongoClient(mongoDbConnection);
            var server = client.GetServer();
            var database = server.GetDatabase("local");
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        protected string Connection { get; set; }

        public virtual bool Add(TEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
        }
            else if (Get(entity.Id) != null)
        {
                Collection.Remove(Query.EQ("_id", entity.Id));
        }

            return Collection.Insert(entity).HasLastErrorMessage;
        }

        public virtual bool Delete(TEntity entity)
            {
            return Collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public virtual IQueryable<TEntity> Get()
        {
            return Collection.AsQueryable();
        }

        public virtual TEntity Get(string id)
            {
            return Collection.FindOneByIdAs<TEntity>(id);
            }

        public virtual IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate.Compile()).AsQueryable();
        }
    }
}