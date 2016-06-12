using AutoMapper;
using BloomService.Domain.Entities.Concrete;
using Sage.WebApi.Models.DbModels;

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
        }
    }
}