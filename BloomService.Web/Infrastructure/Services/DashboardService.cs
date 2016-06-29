using BloomService.Web.Infrastructure.Services.Interfaces;
using BloomService.Web.Models;
using BloomService.Domain.Entities.Concrete;
using AutoMapper;
using System.Collections.Generic;
using BloomService.Domain.Repositories.Abstract;
using System.Linq;
using BloomService.Web.Infrastructure.Constants;

namespace BloomService.Web.Services.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository _repository;

        public DashboardService(IRepository repository)
        {
            _repository = repository;
        }

        public LookupsModel GetLookups()
        {
            var lookups = new LookupsModel();
            var locations = _repository.GetAll<SageLocation>();
            var calltypes = _repository.GetAll<SageCallType>();
            var problems = _repository.GetAll<SageProblem>();
            var employes = _repository.GetAll<SageEmployee>();
            var equipment = _repository.GetAll<SageEquipment>();
            var customer = _repository.GetAll<SageCustomer>();
            var repairs = _repository.GetAll<SageRepair>();
            var ratesheets = _repository.GetAll<SageRateSheet>();
            var permissionCodes = _repository.GetAll<SagePermissionCode>();
            var parts = _repository.GetAll<SagePart>();

            lookups.Locations = Mapper.Map<List<SageLocation>, List<LocationModel>>(locations.ToList());
            lookups.Calltypes = Mapper.Map<List<SageCallType>, List<CallTypeModel>>(calltypes.ToList());
            lookups.Problems = Mapper.Map<List<SageProblem>, List<ProblemModel>>(problems.ToList());
            lookups.Employes = Mapper.Map<List<SageEmployee>, List<EmployeeModel>>(employes.ToList());
            lookups.Equipment = Mapper.Map<List<SageEquipment>, List<EquipmentModel>>(equipment.ToList());
            lookups.Customers = Mapper.Map<List<SageCustomer>, List<CustomerModel>>(customer.ToList());
            lookups.Hours = Mapper.Map<List<SageRepair>, List<RepairModel>>(repairs.ToList());

            lookups.RateSheets = Mapper.Map<List<SageRateSheet>, List<RateSheetModel>>(ratesheets.ToList());
            lookups.PermissionCodes = Mapper.Map<List<SagePermissionCode>, List<PermissionCodeModel>>(permissionCodes.ToList());

            lookups.PaymentMethods = PaymentMethod.PaymentMethodList;
            lookups.Parts = Mapper.Map<List<SagePart>, List<PartModel>>(parts.ToList());

            return lookups;
        }
    }
}