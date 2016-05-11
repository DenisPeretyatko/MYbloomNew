namespace BloomService.Web.Services.Abstract.EntityServices
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Abstract;

    public interface IEntityService<out TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();

        TEntity Get(string id);

        // IEnumerable<TEntity> Delete(PropertyDictionary properties);
    }
}