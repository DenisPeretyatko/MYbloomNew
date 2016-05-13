namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract.EntityServices;

    public interface IAssignmentService : IAddableEntityService<SageAssignment>, IEditableEntityService<SageAssignment>
    {
        SageAssignment GetByWorkOrderId(string id);
    }
}