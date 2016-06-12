namespace BloomService.Web
{
    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<SageLocation, LocationModel>().ReverseMap();
            Mapper.CreateMap<SageCallType, CallTypeModel>().ReverseMap();
            Mapper.CreateMap<SageEmployee, EmployeeModel>().ReverseMap();
            Mapper.CreateMap<SageProblem, ProblemModel>().ReverseMap();
            Mapper.CreateMap<SageEquipment, EquipmentModel>().ReverseMap();
            Mapper.CreateMap<SageCustomer, CustomerModel>().ReverseMap();
            Mapper.CreateMap<SageRepair, RepairModel>().ReverseMap();
            Mapper.CreateMap<SageAssignment, AssignmentModel>().ReverseMap();
            Mapper.CreateMap<SageWorkOrder, WorkorderViewModel>().ReverseMap();
            Mapper.CreateMap<SageWorkOrder, WorkOrderModel>().ReverseMap();
            Mapper.CreateMap<AvailableDay, SageAvailableDay>().ReverseMap();

            Mapper.CreateMap<AssignmentViewModel, SageAssignment>().ReverseMap();
        }
    }
}