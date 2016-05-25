namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using Models.Request;
    public interface IApiMobileService
    {
        bool AddImage(ImageRequest model);

        IEnumerable<SageWorkOrder> GetWorkOreders();

        SageTechnicianLocation SaveTechnicianLocation(string technicianId, decimal lat, decimal lng);
    }
}