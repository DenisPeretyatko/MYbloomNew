namespace BloomService.Web.Services.Abstract
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using Models.Request;

    public interface IApiMobileService
    {
        bool AddImage(ImageModel model);

        IEnumerable<SageWorkOrder> GetWorkOreders();
        IEnumerable<SageEquipment> GetEquipments();
        SageTechnicianLocation SaveTechnicianLocation(string technicianId, decimal lat, decimal lng);
    }
}