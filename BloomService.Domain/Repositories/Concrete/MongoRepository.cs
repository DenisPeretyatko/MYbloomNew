using BloomService.Domain.Entities;
using BloomService.Domain.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BloomService.Domain.Repositories.Concrete
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected string Connection { get; set; } 
        private MongoDatabase database;
        private MongoCollection<TEntity> collection;

        public MongoRepository(string collectionName)
        {
            Connection = "mongodb://localhost/BloomServiceCacheStorage";
            var client = new MongoClient(Connection);
            var server = client.GetServer();
            database = server.GetDatabase("local");
            collection = database.GetCollection<TEntity>(collectionName);
        }

        public bool Insert(TEntity entity)
        {
            if (entity.Id == null)
                entity.Id = ObjectId.GenerateNewId().ToString();
            return collection.Insert(entity).HasLastErrorMessage;
        }

        public bool Update(TEntity entity)
        {
            if (SearchFor(x => x.Id == entity.Id).Any())
                collection.Remove(Query.EQ("_id", entity.Id));
            collection.Insert(entity);
            return true;
        }

        public bool Delete(TEntity entity)
        {
            return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable().Where(predicate.Compile()).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(ObjectId id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }
    }
}
