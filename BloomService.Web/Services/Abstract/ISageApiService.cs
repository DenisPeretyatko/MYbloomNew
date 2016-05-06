namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete;

    public interface ISageApiService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();


        TEntity Get(string id);


        IEnumerable<TEntity> Add(SagePropertyDictionary properties);


        IEnumerable<TEntity> Edit(SagePropertyDictionary properties);

        IEnumerable<TEntity> Delete(SagePropertyDictionary properties);
    }
}
