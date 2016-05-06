namespace BloomService.Web.Managers.Abstract.EntityManagers
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;

    public interface IEntityApiManager<TEntity>
        where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(string endPoint);

        TEntity Get(string endPoint, string id);
    }
}