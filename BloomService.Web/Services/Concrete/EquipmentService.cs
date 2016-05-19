namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class EquipmentService : EntityService<SageEquipment>, IEquipmentService
    {
        private readonly IEquipmentApiManager equipmentApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public EquipmentService(IUnitOfWork unitOfWork, IEquipmentApiManager equipmentApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, equipmentApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.equipmentApiManager = equipmentApiManager;
            Repository = unitOfWork.Equipment;
            _settings = bloomConfiguration;

            EndPoint = _settings.EquipmentEndPoint;
        }
    }
}