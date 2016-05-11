namespace BloomService.Web
{
    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<SageLocation, LocationModel>();
            Mapper.CreateMap<SageCallType, CallTypeModel>();
            Mapper.CreateMap<SageEmployee, EmployeeModel>().ReverseMap();
            Mapper.CreateMap<SageProblem, ProblemModel>();
            Mapper.CreateMap<SageEquipment, EquipmentModel>();
            Mapper.CreateMap<SageCustomer, CustomerModel>();
            Mapper.CreateMap<SageRepair, RepairModel>();
            Mapper.CreateMap<SageAssignment, AssignmentModel>();
            Mapper.CreateMap<SageWorkOrder, WorkorderViewModel>();

            Mapper.CreateMap<AvailableDay, SageAvailableDay>().ReverseMap();
        }
    }
}