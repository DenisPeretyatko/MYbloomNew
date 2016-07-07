namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    public interface IApiMobileService
    {
        bool AddImage(ImageModel model);

        IEnumerable<SageWorkOrder> GetWorkOreders();
        IEnumerable<SageEquipment> GetEquipments();
        SageTechnicianLocation SaveTechnicianLocation(string technicianId, decimal lat, decimal lng);
    }
}