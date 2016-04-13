using BloomService.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BloomService.Domain.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        bool Insert(TEntity entity);


        bool Update(TEntity entity);


        bool Delete(TEntity entity);


        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);


        IEnumerable<TEntity> GetAll();


        TEntity GetById(ObjectId id);
        
    }
}
