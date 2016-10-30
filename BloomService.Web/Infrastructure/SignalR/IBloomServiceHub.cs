﻿using BloomService.Domain.Entities.Concrete;
using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.SignalR
{
    public interface IBloomServiceHub
    {
        void SendNotification(NotificationModel model);
        void CreateAssignment(MapViewModel model);
        void DeleteAssigment(AssignmentViewModel id);
        void CreateWorkOrder(SageWorkOrder model);
        void UpdateWorkOrder(WorkOrderModel model);
        void UpdateTechnician(TechnicianModel model);
        void UpdateTechnicianLocation(SageEmployee model);
        void UpdateWorkOrderPicture(SageImageWorkOrder model);
        void UpdateSageWorkOrder(SageWorkOrder model);
        void ShowAlert(SweetAlertModel model);
    }
}
