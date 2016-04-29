namespace BloomService.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using BloomService.Domain.Entities;
    using BloomService.Web.Services.Abstract;

    public class EquipmentController : ApiController
    {
        private readonly IEquipmentSageApiService equipmentSageApiService;

        public EquipmentController(IEquipmentSageApiService equipmentSageApiService)
        {
            this.equipmentSageApiService = equipmentSageApiService;
        }

        public IEnumerable<SageEquipment> Get()
        {
            return equipmentSageApiService.Get();
        }

        public SageEquipment Get(string id)
        {
            return equipmentSageApiService.Get(id);
        }

        public IEnumerable<SageEquipment> Post(PropertyDictionary properties)
        {
            return equipmentSageApiService.Add(properties);
        }

        public IEnumerable<SageEquipment> Put(PropertyDictionary properties)
        {
            return equipmentSageApiService.Edit(properties);
        }

        public IEnumerable<SageEquipment> Delete(PropertyDictionary properties)
        {
            return equipmentSageApiService.Edit(properties);
        }
    }
}
