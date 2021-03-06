﻿namespace BloomService.Web
{
    using AutoMapper;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Models;
    using Infrastructure.Constants;
    using System;
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

            Mapper.CreateMap<SageRateSheet, RateSheetModel>().ReverseMap();
            Mapper.CreateMap<SagePermissionCode, PermissionCodeModel>().ReverseMap();

            Mapper.CreateMap<AvailableDay, SageAvailableDay>().ReverseMap();

            Mapper.CreateMap<AssignmentViewModel, SageAssignment>().ReverseMap();
            Mapper.CreateMap<SagePart, PartModel>().ReverseMap();

            Mapper.CreateMap<SageWorkOrderItem, LaborPartsModel>()
                .ForMember(dest => dest.WorkDate, opts => opts.MapFrom(src => src.WorkDate))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.ItemType))
                .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.UnitSale))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.BiledQty, opts => opts.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.CostQty, opts => opts.MapFrom(src => src.CostQuantity))
                .ForMember(dest => dest.Employee, opts => opts.MapFrom(src => src.Employee))
                .ReverseMap();

            Mapper.CreateMap<LaborPartsModel, SageWorkOrderItem>()
              .ForMember(dest => dest.WorkDate, opts => opts.MapFrom(src => src.WorkDate))
              .ForMember(dest => dest.ItemType, opts => opts.MapFrom(src => src.Type))
              .ForMember(dest => dest.UnitSale, opts => opts.MapFrom(src => src.Rate))
              .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
              .ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.BiledQty))
              .ForMember(dest => dest.CostQuantity, opts => opts.MapFrom(src => src.CostQty))
              .ForMember(dest => dest.Employee, opts => opts.MapFrom(src => src.Employee))
              .ForMember(dest => dest.Repair, opts => opts.MapFrom(src => Convert.ToInt32(src.LaborItem.Repair)))
              .ReverseMap();


            Mapper.CreateMap<WorkOrderNoteModel, SageNote>()
             .ForMember(dest => dest.TYPE, opts => opts.UseValue(3))
             .ForMember(dest => dest.TRANSNBR, opts => opts.MapFrom(src => src.WorkOrderId))
             .ForMember(dest => dest.TRANSNBR2, opts => opts.UseValue(0))
             .ForMember(dest => dest.TRANSNBR3, opts => opts.UseValue(0))
             .ForMember(dest => dest.DATE, opts => opts.UseValue(DateTime.Now))
             .ForMember(dest => dest.EMPLOYEENBR, opts => opts.UseValue(0))
             .ForMember(dest => dest.DATEENTER, opts => opts.UseValue(DateTime.Now))
             .ForMember(dest => dest.FORMAT, opts => opts.UseValue(0))
             .ForMember(dest => dest.DATABASENBR, opts => opts.UseValue(0))
             .ForMember(dest => dest.NOTENBR, opts => opts.MapFrom(src => src.NoteId))
             .ForMember(dest => dest.QSYSGEN, opts => opts.UseValue("N"))
             .ForMember(dest => dest.QCUSTVIEW, opts => opts.UseValue("Y"))
             .ForMember(dest => dest.SUBJECTLINE, opts => opts.MapFrom(src => src.SubjectLine))
             .ForMember(dest => dest.TEXT, opts => opts.MapFrom(src => src.Text));

            Mapper.CreateMap<SageNote, WorkOrderNoteModel>()
             .ForMember(dest => dest.WorkOrderId, opts => opts.MapFrom(src => src.TRANSNBR))
             .ForMember(dest => dest.NoteId, opts => opts.MapFrom(src => src.NOTENBR))
             .ForMember(dest => dest.SubjectLine, opts => opts.MapFrom(src => src.SUBJECTLINE))
             .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.TEXT));
        }
    }
}