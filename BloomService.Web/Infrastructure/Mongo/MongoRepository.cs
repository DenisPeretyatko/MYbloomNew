namespace BloomService.Web.Infrastructure.Mongo
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Attributes;
    using BloomService.Domain.Entities.Abstract;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using Utils;
    using Domain.Entities.Concrete;
    using System.Collections.Generic;
    public class MongoRepository : IRepository
    {
        private readonly MongoDatabase database;

        public MongoRepository(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase(dbName);
        }

        public virtual bool Add<TEntity>(TEntity entity) where TEntity : IEntity
        {
            var collection = GetCollection<TEntity>();
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString(); ;
            }
            else if (Get<TEntity>(entity.Id) != null)
            {
                GetCollection<TEntity>().Remove(Query.EQ("_id", entity.Id));
            }

            return GetCollection<TEntity>().Insert(entity).HasLastErrorMessage;
        }        

        public virtual void AddMany<TEntity>(List<TEntity> entities) where TEntity : IEntity
        {
            var collection = GetCollection<TEntity>();
            foreach (var item in entities)
            {
                if (item.Id == null)
                    item.Id = ObjectId.GenerateNewId().ToString();
            }
            GetCollection<TEntity>().InsertBatch(entities);
        }

        public virtual bool Delete<TEntity>(TEntity entity) where TEntity : IEntity
        {
            return GetCollection<TEntity>().Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public virtual TEntity Get<TEntity>(string id) where TEntity : IEntity
        {
            return GetCollection<TEntity>().FindOneByIdAs<TEntity>(id);
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>() where TEntity : IEntity
        {
            return GetCollection<TEntity>().AsQueryable();
        }

        public virtual IQueryable<TEntity> SearchFor<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : IEntity
        {
            return predicate == null ? GetAll<TEntity>() : GetCollection<TEntity>().AsQueryable().Where(predicate);
        }

        public virtual bool Update<TEntity>(TEntity item) where TEntity : IEntity
        {
            return string.IsNullOrEmpty(item.Id)
                       ? Add(item)
                       : GetCollection<TEntity>().Save<TEntity>(item).HasLastErrorMessage;
        }

        void IRepository.Delete<TEntity>(TEntity item)
        {
            var query = Query<TEntity>.EQ(e => e.Id, item.Id);
            GetCollection<TEntity>().Remove(query);
        }      

        private MongoCollection<TEntity> GetCollection<TEntity>() where TEntity : IEntity
        {
            var collectionNameAttribute =
                typeof(TEntity).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault() as
                CollectionNameAttribute;
            return database.GetCollection<TEntity>(collectionNameAttribute.Name);
        }
    }
}