namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;

    public interface IApiMobileService
    {
        bool AddImage(IEnumerable<string> images, string idWorkOrder);

        IEnumerable<SageWorkOrder> GetWorkOreders();
    }
}