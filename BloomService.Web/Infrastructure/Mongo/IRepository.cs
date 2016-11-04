namespace BloomService.Web.Infrastructure.Mongo
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using BloomService.Domain.Entities.Abstract;
    using System.Collections.Generic;
    public interface IRepository
    {
        bool Add<TEntity>(TEntity item) where TEntity : IEntity;

        void AddMany<TEntity>(List<TEntity> entities) where TEntity : IEntity;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : IEntity;

        TEntity Get<TEntity>(string id) where TEntity : IEntity;

        IQueryable<TEntity> SearchFor<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : IEntity;

        bool Update<TEntity>(TEntity item) where TEntity : IEntity;

        void Delete<TEntity>(TEntity item) where TEntity : IEntity;
    }
}