using System.Collections.Generic;

namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities.Concrete;

    public interface IWorkOrderService : IEntityService<SageWorkOrder>
    {
        SageImageWorkOrder GetPictures(string id);
    }
}