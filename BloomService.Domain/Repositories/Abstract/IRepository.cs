namespace BloomService.Domain.Repositories.Abstract
{
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

    using BloomService.Domain.Entities.Abstract;

    public interface IRepository<TEntity>
        where TEntity : IEntity
{
        IEnumerable<TEntity> Get();

        TEntity Get(string id);

        bool Insert(TEntity item);

        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

        bool Update(TEntity entity);
    }
}
