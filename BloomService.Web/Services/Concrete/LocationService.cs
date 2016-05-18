namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;

    public class LocationService : EntityService<SageLocation>, ILocationService
    {
        private readonly ILocationApiManager locationApiManager;

        private readonly IUnitOfWork unitOfWork;

        public LocationService(IUnitOfWork unitOfWork, ILocationApiManager locationApiManager)
            : base(unitOfWork, locationApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.locationApiManager = locationApiManager;
            Repository = unitOfWork.Locations;
            EndPoint = ConfigurationManager.AppSettings["LocationEndPoint"];
        }
    }
}