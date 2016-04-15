﻿namespace BloomService.Web.Services.Concrete
{
    using System.Configuration;

    using BloomService.Domain.Entities;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Utils;

    using RestSharp;

    public class EquipmentSageApiService : SageApiService<SageEquipment>, IEquipmentSageApiService
    {
        private IRestClient restClient;

        private ISession session;

        private IUnitOfWork unitOfWork;

        public EquipmentSageApiService(IRestClient restClient, IUnitOfWork unitOfWork /*, ISession session*/)
            : base(restClient, unitOfWork /*, session*/)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;

            // this.session = session;
            EndPoint = ConfigurationManager.AppSettings["EquipmentEndPoint"];
        }
    }
}