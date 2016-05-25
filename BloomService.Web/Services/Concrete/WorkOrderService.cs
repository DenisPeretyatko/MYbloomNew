namespace BloomService.Web.Services.Concrete
{
    using System;
    using System.Collections.Generic;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.Entities.Concrete.Auxiliary;
    using BloomService.Domain.Extensions;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class WorkOrderService : EntityService<SageWorkOrder>, IWorkOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IWorkOrderApiManager workOrderApiManager;

        public WorkOrderService(
            IUnitOfWork unitOfWork, 
            IWorkOrderApiManager workOrderApiManager, 
            BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, workOrderApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.workOrderApiManager = workOrderApiManager;
            Repository = unitOfWork.WorkOrders;
            EndPoint = bloomConfiguration.WorkOrderEndPoint;
        }

        public SageWorkOrder Add(SageWorkOrder workOrder)
        {
            var response = workOrderApiManager.Add(EndPoint, workOrder);
            if (response.IsSucceed)
            {
                unitOfWork.WorkOrders.Add(workOrder);
                unitOfWork.Changes.Add(
                    new SageChange
                        {
                            Change = ChangeType.Create, 
                            EntityId = workOrder.WorkOrder, 
                            EntityType = GetEntityName(), 
                            Status = StatusType.NotSynchronized, 
                            ChangeTime = DateTime.UtcNow
                        });
            }

            var result = (SageWorkOrder)response.Entity;
            return result;
        }

        public override IEnumerable<SageWorkOrder> Edit(Dictionary<string, string> properties)
        {
            var result = workOrderApiManager.Add(EndPoint, properties);
            var workOrder = new SageWorkOrder
                                {
                                    Id = properties["Id"], 
                                    ARCustomer = properties["ARCustomer"], 
                                    Location = properties["Location"], 
                                    CallType = properties["CallType"], 
                                    // CallDate = DateTime.Parse(properties["CallDate"]),
                                    // CallTime = DateTime.Parse(properties["CallTime"]),
                                    // Problem = properties["Problem"],
                                    // RateSheet = properties["RateSheet"],
                                    // Employee = properties["Employee"],
                                    // Equipment = Convert.ToUInt16(properties["Equipment"]),
                                    // EstimatedRepairHours =
                                    // Convert.ToInt32(properties["EquipmentEstimatedRepairHours"]),
                                    // NottoExceed = properties["NottoExceed"],
                                    Comments = properties["Comments"], 
                                    CustomerPO = properties["CustomerPO"], 
                                    PermissionCode = properties["PermissionCode"], 
                                    PayMethod = properties["PayMethod"]
                                };

            unitOfWork.WorkOrders.Add(workOrder);

            unitOfWork.Changes.Add(
                new SageChange
                    {
                        Change = ChangeType.Create, 
                        EntityId = GetEntityId(properties), 
                        EntityType = GetEntityName(), 
                        Status = StatusType.NotSynchronized, 
                        ChangeTime = DateTime.UtcNow
                    });

            return result;
        }

        public IEnumerable<SageWorkOrder> UnAssign(string id)
        {
            return workOrderApiManager.Delete(EndPoint, id);
        }
    }
}