namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface ISageApiService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();


        TEntity Get(string id);


        IEnumerable<TEntity> Add(PropertyDictionary properties);


        IEnumerable<TEntity> Edit(PropertyDictionary properties);

        IEnumerable<TEntity> Delete(PropertyDictionary properties);
    }
}
