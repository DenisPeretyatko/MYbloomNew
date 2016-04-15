namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities;

    public interface ISageApiService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();


        TEntity Get(string id);


        IEnumerable<TEntity> Add(Properties properties);


        IEnumerable<TEntity> Edit(Properties properties);

        IEnumerable<TEntity> Delete(Properties properties);
    }
}
