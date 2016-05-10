namespace BloomService.Domain.Repositories.Abstract
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Entities.Abstract;

    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        bool Add(TEntity item);

        IQueryable<TEntity> Get();

        TEntity Get(string id);

        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
    }
}