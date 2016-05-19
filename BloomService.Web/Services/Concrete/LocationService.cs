namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Managers.Abstract;
    using BloomService.Web.Services.Abstract;
    using Domain.Extensions;

    public class LocationService : EntityService<SageLocation>, ILocationService
    {
        private readonly ILocationApiManager locationApiManager;

        private readonly IUnitOfWork unitOfWork;

        private readonly BloomServiceConfiguration _settings;

        public LocationService(IUnitOfWork unitOfWork, ILocationApiManager locationApiManager, BloomServiceConfiguration bloomConfiguration)
            : base(unitOfWork, locationApiManager)
        {
            this.unitOfWork = unitOfWork;
            this.locationApiManager = locationApiManager;
            Repository = unitOfWork.Locations;
            _settings = bloomConfiguration;
            EndPoint = _settings.LocationEndPoint;
        }
    }
}