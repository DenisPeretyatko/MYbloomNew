namespace BloomService.Web.Services.Abstract.EntityServices
{
    using BloomService.Domain.Entities.Abstract;

    public interface IAddableEditableEntityService<TEntity> : IAddableEntityService<TEntity>, 
                                                              IEditableEntityService<TEntity>
        where TEntity : class, IEntity
    {
    }
}