namespace BloomService.Web.Managers.Abstract.EntityManagers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;

    public interface IAddableEntityApiManager<TEntity> : IEntityApiManager<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Add(string endPoint, SagePropertyDictionary properties);
    }
}