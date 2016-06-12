namespace BloomService.Domain.Repositories.Abstract
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Entities.Abstract;

    public interface IRepository
    {
        bool Add<TEntity>(TEntity item) where TEntity : IEntity;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : IEntity;

        TEntity Get<TEntity>(string id) where TEntity : IEntity;

        IQueryable<TEntity> SearchFor<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : IEntity;

        bool Update<TEntity>(TEntity item) where TEntity : IEntity;

        void Delete<TEntity>(TEntity item) where TEntity : IEntity;
    }
}