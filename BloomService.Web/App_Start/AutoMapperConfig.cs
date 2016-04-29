using BloomService.Domain.Entities;
using BloomService.Web.Models;

namespace BloomService.Web
{
    public static class AutoMapperConfig
{
    public static void RegisterMappings()
    {
        AutoMapper.Mapper.CreateMap<SageLocation, LocationModel>();
        AutoMapper.Mapper.CreateMap<SageCallType, CallTypeModel>();
        AutoMapper.Mapper.CreateMap<SageEmployee, EmployeeModel>();
        AutoMapper.Mapper.CreateMap<SageProblem, ProblemModel>();
        AutoMapper.Mapper.CreateMap<SageEquipment, EquipmentModel>();
        AutoMapper.Mapper.CreateMap<SageCustomer, CustomerModel>();
        AutoMapper.Mapper.CreateMap<SageRepair, RepairModel>();
        AutoMapper.Mapper.CreateMap<SageAssignment, AssignmentModel>();
        AutoMapper.Mapper.CreateMap<SageWorkOrder, WorkorderViewModel>();
    }
}
}