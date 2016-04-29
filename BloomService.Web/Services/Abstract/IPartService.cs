namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities;
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract.EntityServices;

    public interface IPartService : IEntityService<SagePart>
    {
    }
}