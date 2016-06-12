﻿using System.Collections.Generic;

namespace BloomService.Web.Services.Abstract
{
    using BloomService.Domain.Entities.Concrete;
    using Domain.Entities.Concrete.Auxiliary;
    using System.Collections.Generic;
    public interface IWorkOrderService : IEntityService<SageWorkOrder>
    {
        SageImageWorkOrder GetPictures(string id);
        SageResponse<SageWorkOrder> AddEquipment(Dictionary<string, string> properties);

        IEnumerable<SageWorkOrder> UnAssign(string id);
    }
}