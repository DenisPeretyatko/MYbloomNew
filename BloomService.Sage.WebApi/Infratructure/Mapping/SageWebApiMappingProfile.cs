using AutoMapper;
using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Entities.Concrete.ReturnParamModels;
using BloomService.Domain.Extensions;
using Sage.WebApi.Models.DbModels;
using System;

namespace Sage.WebApi.Mapping
{
    public class SageWebApiMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WorkOrderDbModel, SageWorkOrder>()
                .ForMember(dest => dest.ActualLaborHours, opts => opts.MapFrom(src => src.ACTLABORHOURS))
                .ForMember(dest => dest.ActualMiscCost, opts => opts.MapFrom(src => src.ACTMISCCOST))
                .ForMember(dest => dest.ActualPartsCost, opts => opts.MapFrom(src => src.ACTPARTSCOST))
                .ForMember(dest => dest.Agreement, opts => opts.MapFrom(src => src.AGREEMENTNBR))
                .ForMember(dest => dest.AlternateWorkOrderNbr, opts => opts.MapFrom(src => src.ALTWONBR))
                .ForMember(dest => dest.AmountBilled, opts => opts.MapFrom(src => src.AMTBILLED))
                .ForMember(dest => dest.ARCustomer, opts => opts.MapFrom(src => src.ARCUST))
                .ForMember(dest => dest.Area, opts => opts.MapFrom(src => src.AREA))
                .ForMember(dest => dest.CallDate, opts => opts.MapFrom(src => src.CALLDATE))
                .ForMember(dest => dest.CallTime, opts => opts.MapFrom(src => src.CALLTIME))
                .ForMember(dest => dest.CallType, opts => opts.MapFrom(src => src.CALLTYPECODE))
                .ForMember(dest => dest.Center, opts => opts.MapFrom(src => src.CENTERNBR))
                .ForMember(dest => dest.ChargeBillto, opts => opts.MapFrom(src => src.CHARGEBILLTO))
                .ForMember(dest => dest.Comments, opts => opts.MapFrom(src => src.COMMENTS))
                .ForMember(dest => dest.CompletedBy, opts => opts.MapFrom(src => src.COMPLETEBY))
                .ForMember(dest => dest.Contact, opts => opts.MapFrom(src => src.CONTACT))
                .ForMember(dest => dest.CustomerPO, opts => opts.MapFrom(src => src.CUSTOMERPO))
                .ForMember(dest => dest.DateClosed, opts => opts.MapFrom(src => src.DATECLOSED))
                .ForMember(dest => dest.DateComplete, opts => opts.MapFrom(src => src.DATECOMPLETE))
                .ForMember(dest => dest.DateEntered, opts => opts.MapFrom(src => src.DATEENTER))
                .ForMember(dest => dest.DateRun, opts => opts.MapFrom(src => src.DATERUN))
                .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.DEPT))
                .ForMember(dest => dest.EnteredBy, opts => opts.MapFrom(src => src.ENTERBY))
                .ForMember(dest => dest.EstimatedLaborCost, opts => opts.MapFrom(src => src.ESTLABORCOST))
                .ForMember(dest => dest.EstimatedMiscCost, opts => opts.MapFrom(src => src.ESTMISCCOST))
                .ForMember(dest => dest.EstimatedPartsCost, opts => opts.MapFrom(src => src.ESTPARTSCOST))
                .ForMember(dest => dest.EstimatedRepairHours, opts => opts.MapFrom(src => src.ESTREPAIRHRS))
                .ForMember(dest => dest.InvoiceDate, opts => opts.MapFrom(src => src.INVOICEDATE))
                .ForMember(dest => dest.JCExtra, opts => opts.MapFrom(src => src.JCEXTRA))
                .ForMember(dest => dest.JCJob, opts => opts.MapFrom(src => src.JCJOB))
                .ForMember(dest => dest.JobSaleProduct, opts => opts.MapFrom(src => src.JOBSALEPRODUCT))
                .ForMember(dest => dest.Lead, opts => opts.MapFrom(src => src.LEADNBR))
                .ForMember(dest => dest.LeadSource, opts => opts.MapFrom(src => src.LEADSRCNBR))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.SERVSITENBR))
                .ForMember(dest => dest.Misc, opts => opts.MapFrom(src => src.MISC))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.NAME))
                .ForMember(dest => dest.NottoExceed, opts => opts.MapFrom(src => src.NOTTOEXCEED))
                .ForMember(dest => dest.PayMethod, opts => opts.MapFrom(src => src.PAYMETHOD))
                .ForMember(dest => dest.PermissionCode, opts => opts.MapFrom(src => src.PERMISSIONCODE))
                .ForMember(dest => dest.Priority, opts => opts.MapFrom(src => src.PRIORITY))
                .ForMember(dest => dest.Problem, opts => opts.MapFrom(src => src.PROBLEMCODE))
                .ForMember(dest => dest.PreventiveMaintenance, opts => opts.MapFrom(src => src.QPM))
                .ForMember(dest => dest.QuoteExpirationDate, opts => opts.MapFrom(src => src.QUOTEEXPDATE))
                .ForMember(dest => dest.RateSheet, opts => opts.MapFrom(src => src.RATESHEETNBR))
                .ForMember(dest => dest.SalesEmployee, opts => opts.MapFrom(src => src.SALESMANNBR))
                .ForMember(dest => dest.SalesTaxAmount, opts => opts.MapFrom(src => src.SALESTAXAMT))
                .ForMember(dest => dest.SalesTaxBilled, opts => opts.MapFrom(src => src.SALESTAXBILLED))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.STATUS))
                .ForMember(dest => dest.Employee, opts => opts.MapFrom(src => src.TECHNICIAN))
                .ForMember(dest => dest.TimeComplete, opts => opts.MapFrom(src => src.TIMECOMPLETE))
                .ForMember(dest => dest.TimeEntered, opts => opts.MapFrom(src => src.TIMEENTER))
                .ForMember(dest => dest.TotalCost, opts => opts.MapFrom(src => src.TOTALCOST))
                .ForMember(dest => dest.WorkOrderType, opts => opts.MapFrom(src => src.WOTYPE))
                .ForMember(dest => dest.WorkOrder, opts => opts.MapFrom(src => src.WRKORDNBR));

            CreateMap<AssignmentReturnParam, SageAssignment>()
                .ForMember(dest => dest.ElapsedTime, opts => opts.MapFrom(src => src.ElapsedTime.AsDoubleSafe()))
                .ForMember(dest => dest.CreateTimeEntry, opts => opts.MapFrom(src => ConvertToTime(src.CreateTimeEntry)))
                .ForMember(dest => dest.AlarmDate, opts => opts.MapFrom(src => ConvertToDate(src.AlarmDate)))
                .ForMember(dest => dest.DateEntered, opts => opts.MapFrom(src => ConvertToDate(src.DateEntered)))
                .ForMember(dest => dest.Enddate, opts => opts.MapFrom(src => ConvertToDate(src.Enddate)))
                .ForMember(dest => dest.Endtime, opts => opts.MapFrom(src => ConvertToTime(src.Endtime)))
                .ForMember(dest => dest.ETAdate, opts => opts.MapFrom(src => ConvertToDate(src.ETAdate)))
                .ForMember(dest => dest.ETAtime, opts => opts.MapFrom(src => ConvertToTime(src.ETAtime)))
                .ForMember(dest => dest.LastTime, opts => opts.MapFrom(src => ConvertToTime(src.LastTime)))
                .ForMember(dest => dest.LastDate, opts => opts.MapFrom(src => ConvertToDate(src.LastDate)))
                .ForMember(dest => dest.PostedTime, opts => opts.MapFrom(src => ConvertToTime(src.PostedTime)))
                .ForMember(dest => dest.ScheduleDate, opts => opts.MapFrom(src => ConvertToDate(src.ScheduleDate)))
                .ForMember(dest => dest.StartTime, opts => opts.MapFrom(src => ConvertToTime(src.StartTime)))
                .ForMember(dest => dest.TimeEntered, opts => opts.MapFrom(src => ConvertToTime(src.TimeEntered)));

            CreateMap<LocationReturnParam, SageLocation>()
                .ForMember(dest => dest.AccountOpenDate, opts => opts.MapFrom(src => ConvertToDate(src.AccountOpenDate)));

            CreateMap<BloomService.Domain.Entities.Concrete.ReturnParamModels.EquipmentReturnParam, SageEquipment>()
                .ForMember(dest => dest.DateRemoved, (IMemberConfigurationExpression<BloomService.Domain.Entities.Concrete.ReturnParamModels.EquipmentReturnParam> opts) => opts.MapFrom(src => ConvertToDate(src.DateRemoved)))
                .ForMember(dest => dest.DateReplaced, (IMemberConfigurationExpression<BloomService.Domain.Entities.Concrete.ReturnParamModels.EquipmentReturnParam> opts) => opts.MapFrom(src => ConvertToDate(src.DateReplaced)))
                .ForMember(dest => dest.InstallDate, (IMemberConfigurationExpression<BloomService.Domain.Entities.Concrete.ReturnParamModels.EquipmentReturnParam> opts) => opts.MapFrom(src => ConvertToDate(src.InstallDate)));

            CreateMap<EmployeeReturnParam, SageEmployee>()
               .ForMember(dest => dest.Birthdate, opts => opts.MapFrom(src => ConvertToDate(src.Birthdate)))
               .ForMember(dest => dest.DefaultStartTime, opts => opts.MapFrom(src => ConvertToTime(src.DefaultStartTime)))
               .ForMember(dest => dest.NormalEndTime, opts => opts.MapFrom(src => ConvertToTime(src.NormalEndTime)));

            CreateMap<CustomerReturnParam, SageCustomer>()
               .ForMember(dest => dest.Checklist1Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist1Date)))
               .ForMember(dest => dest.Checklist10Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist10Date)))
               .ForMember(dest => dest.Checklist2Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist2Date)))
               .ForMember(dest => dest.Checklist3Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist3Date)))
               .ForMember(dest => dest.Checklist4Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist4Date)))
               .ForMember(dest => dest.Checklist5Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist5Date)))
               .ForMember(dest => dest.Checklist6Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist6Date)))
               .ForMember(dest => dest.Checklist7Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist7Date)))
               .ForMember(dest => dest.Checklist8Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist8Date)))
               .ForMember(dest => dest.Checklist9Date, opts => opts.MapFrom(src => ConvertToDate(src.Checklist9Date)))
               .ForMember(dest => dest.DateEstablished, opts => opts.MapFrom(src => ConvertToDate(src.DateEstablished)))
               //.ForMember(dest => dest.TimeStamp, opts => opts.MapFrom(src => ConvertToTime(src.TimeStamp)))
               .ForMember(dest => dest.DateStamp, opts => opts.MapFrom(src => ConvertToDate(src.DateStamp)));

            CreateMap<PartReturnParam, SagePart>()
              .ForMember(dest => dest.LastActivityDate, opts => opts.MapFrom(src => ConvertToDate(src.LastActivityDate)))
              .ForMember(dest => dest.LastOrderDate, opts => opts.MapFrom(src => ConvertToDate(src.LastOrderDate)))
              .ForMember(dest => dest.LastReceivedDate, opts => opts.MapFrom(src => ConvertToDate(src.LastReceivedDate)))
              .ForMember(dest => dest.OrderLeadTime, opts => opts.MapFrom(src => ConvertToTime(src.OrderLeadTime)));

            CreateMap<WorkOrderReturnParam, SageWorkOrder>()
             .ForMember(dest => dest.CallTime, opts => opts.MapFrom(src => ConvertToTime(src.CallTime)))
             .ForMember(dest => dest.CallDate, opts => opts.MapFrom(src => ConvertToDate(src.CallDate)))
             .ForMember(dest => dest.DateClosed, opts => opts.MapFrom(src => ConvertToDate(src.DateClosed)))
             .ForMember(dest => dest.DateComplete, opts => opts.MapFrom(src => ConvertToDate(src.DateComplete)))
             .ForMember(dest => dest.DateEntered, opts => opts.MapFrom(src => ConvertToDate(src.DateEntered)))
             .ForMember(dest => dest.DateRun, opts => opts.MapFrom(src => ConvertToDate(src.DateRun)))
             .ForMember(dest => dest.InvoiceDate, opts => opts.MapFrom(src => ConvertToDate(src.InvoiceDate)))
             .ForMember(dest => dest.QuoteExpirationDate, opts => opts.MapFrom(src => ConvertToDate(src.QuoteExpirationDate)))
             .ForMember(dest => dest.TimeComplete, opts => opts.MapFrom(src => ConvertToTime(src.TimeComplete)))
             .ForMember(dest => dest.TimeEntered, opts => opts.MapFrom(src => ConvertToTime(src.TimeEntered)));
        }

        private static DateTime ConvertToDate(string dateString)
        {
            DateTime result;

            if (DateTime.TryParse(dateString, out result))
            {
                return result.ToUniversalTime().Date;
            }
            return new DateTime(2000, 1, 1).ToUniversalTime().Date;
        }

        private static DateTime ConvertToTime(string timeString)
        {
            DateTime result;

            if (DateTime.TryParse(timeString, out result))
            {
                return result.ToUniversalTime();
            }
            return new DateTime(2000, 1, 1).ToUniversalTime();
        }
    }
}