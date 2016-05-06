namespace BloomService.Web.Services.Abstract.EntityServices
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;

    public interface IEditableEntityService<out TEntity> : IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Edit(SagePropertyDictionary properties);
    }
}