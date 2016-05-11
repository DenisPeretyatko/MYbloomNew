namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Services.Abstract.EntityServices;

    public interface IEmployeeService : IAddableEntityService<SageEmployee>, IEditableEntityService<SageEmployee>
    {
        IEnumerable<SageEmployee> EditToMongo(SageEmployee employee);
    }
}