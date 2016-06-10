namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;

    public interface IEntityService<out TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Add(SagePropertyDictionary properties);

        IEnumerable<TEntity> Delete(string id);

        IEnumerable<TEntity> Edit(SagePropertyDictionary properties);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> GetPage(int numberPage, string searchString);

        int CountPage(string searchString);

        TEntity Get(string id);
    }
}