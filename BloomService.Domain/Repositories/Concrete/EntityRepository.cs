namespace BloomService.Domain.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
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
        protected readonly MongoCollection<TEntity> collection;

        public EntityRepository(string collectionName)
        {
            //Connection = "mongodb://localhost/BloomServiceCacheStorage";

            var mongoDbConnection = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;

            // Connection = ConfigurationManager.AppSettings["MongoServerSettings"];

            var client = new MongoClient(mongoDbConnection);
            var server = client.GetServer();
            var database = server.GetDatabase("local");
            collection = database.GetCollection<TEntity>(collectionName);
        }

        protected string Connection { get; set; }

        public bool Delete(TEntity entity)
        {
            return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IEnumerable<TEntity> Get()
        {
            return collection.AsQueryable();
        }

        public TEntity Get(string id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }

            return collection.Insert(entity).HasLastErrorMessage;
        }

        public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable().Where(predicate.Compile());
        }

        public bool Update(TEntity entity)
        {
            if (SearchFor(x => x.Id == entity.Id).Any())
            {
                collection.Remove(Query.EQ("_id", entity.Id));
            }
            entity.Id = ObjectId.GenerateNewId().ToString();
            var result = collection.Insert(entity);
            return true;
        }
    }
}