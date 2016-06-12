namespace BloomService.Web.Managers.Abstract
{
    using BloomService.Domain.Entities.Abstract;
    using BloomService.Domain.Entities.Concrete.Auxiliary;

    public interface IEntityApiManager<TEntity>
        where TEntity : IEntity
    {
        string GetEndPoint { get; set; }

        string EditEndPoint { get; set; }

        string DeleteEndPoint { get; set; }

        string CreateEndPoint { get; set; }

        SageResponse<TEntity> Add(TEntity entity);

        SageResponse<TEntity> Delete(string id);

        SageResponse<TEntity> Edit(TEntity entity);

        SageResponse<TEntity> Get();

        SageResponse<TEntity> Get(string id);
    }
}