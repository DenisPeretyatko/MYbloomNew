namespace BloomService.Web.Managers.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;

    public interface IEntityApiManager<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> Add(string endPoint, Dictionary<string, string> properties);

        IEnumerable<TEntity> Delete(string endPoint, string id);

        IEnumerable<TEntity> Edit(string endPoint, Dictionary<string, string> properties);

        IEnumerable<TEntity> Get(string endPoint);

        TEntity Get(string endPoint, string id);
    }
}