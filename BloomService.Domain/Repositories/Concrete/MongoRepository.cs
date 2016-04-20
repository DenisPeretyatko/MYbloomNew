namespace BloomService.Domain.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Repositories.Abstract;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class MongoRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly MongoCollection<TEntity> collection;

        public MongoRepository(string collectionName)
        {
            Connection = "mongodb://localhost/BloomServiceCacheStorage";

            //Connection = ConfigurationManager.AppSettings["MongoServerSettings"];
            var client = new MongoClient(Connection);
            var server = client.GetServer();
            var database = server.GetDatabase("local");
            collection = database.GetCollection<TEntity>(collectionName);
        }

        protected string Connection { get; set; }

        public bool Delete(TEntity entity)
        {
            return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return collection.AsQueryable();
        }

        public TEntity GetById(Int32 id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        public bool Insert(TEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }

            return collection.Insert(entity).HasLastErrorMessage;
        }

        public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable().Where(predicate.Compile()).ToList();
        }

        public bool Update(TEntity entity)
        {
            if (SearchFor(x => x.Id == entity.Id).Any())
            {
                collection.Remove(Query.EQ("_id", entity.Id));
            }

            collection.Insert(entity);
            return true;
        }
    }
}