﻿namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class EquipmentService : EntityService<SageEquipment>, IEquipmentService
    {
        private readonly IEquipmentApiManager equipmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        public EquipmentService(IUnitOfWork unitOfWork, IEquipmentApiManager equipmentApiManager)
            : base(unitOfWork, equipmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.equipmentApiManager = equipmentApiManager;

            EndPoint = ConfigurationManager.AppSettings["EquipmentEndPoint"];
        }
    }
}