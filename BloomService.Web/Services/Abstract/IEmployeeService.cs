namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;

    public interface IEmployeeService : IEntityService<SageEmployee>
    {
        IEnumerable<SageEmployee> EditToMongo(SageEmployee employee);
    }
}