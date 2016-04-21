using System;

namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using BloomService.Domain.Entities;
    using BloomService.Domain.UnitOfWork;
    using BloomService.Web.Services.Abstract;
    using BloomService.Web.Utils;

    using MongoDB.Bson;

    using Newtonsoft.Json.Linq;

    using RestSharp;

    public class SageApiService<TEntity> : ISageApiService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRestClient restClient;

        private readonly ISession session;

        private readonly IUnitOfWork unitOfWork;

        public SageApiService(IRestClient restClient, IUnitOfWork unitOfWork)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;
        }

        public string EndPoint { get; set; }

        public virtual IEnumerable<TEntity> Add(PropertyDictionary properties)
        {
            var request = new RestRequest(EndPoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(properties);
            //request.AddParameter("properties", request.JsonSerializer.Serialize(properties));
            //request.AddObject(properties);
            request.AddHeader("Authorization", GetAuthToken());

            var response = restClient.Execute<List<TEntity>>(request);
            
            var results = response.Data;

            foreach (var result in results)
            {
                unitOfWork.GetEntities<TEntity>().Insert(result);
            }

            return results;
        }

        public virtual IEnumerable<TEntity> Delete(PropertyDictionary properties)
        {
            var request = new RestRequest(EndPoint, Method.DELETE);
            request.AddObject(properties);
            request.AddHeader("Authorization", GetAuthToken());
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        public virtual IEnumerable<TEntity> Edit(PropertyDictionary properties)
        {
            var request = new RestRequest(EndPoint, Method.PUT);
            request.AddObject(properties);
            request.AddHeader("Authorization", GetAuthToken());
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = unitOfWork.GetEntities<TEntity>().GetAll().Take(20).ToArray();

            if (items.Any())
            {
                return items;
            }

            var request = new RestRequest(EndPoint, Method.GET);
            request.AddHeader("Authorization", GetAuthToken());
            var response = restClient.Execute<List<TEntity>>(request);
            var results = response.Data;

            foreach (var result in results)
            {
                unitOfWork.GetEntities<TEntity>().Insert(result);
            }

            return results;
        }

        public virtual TEntity Get(string id)
        {
            var item = unitOfWork.GetEntities<TEntity>().GetById(Int32.Parse(id));

            if (item != null)
            {
                return item;
            }

            var request = new RestRequest(EndPoint, Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", GetAuthToken());
            var response = restClient.Execute(request);
            var result = response.Content as TEntity;

            unitOfWork.GetEntities<TEntity>().Insert(result);

            return result;
        }

        protected string GetAuthToken()
        {
            // if(session.Session["oauth_token"] != null)
            // {
            // return session.Session["oauth_token"].ToString();
            // }
            // else
            // {
            var username = ConfigurationManager.AppSettings["SageUsername"];
            var password = ConfigurationManager.AppSettings["SagePassword"];

            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var json = JObject.Parse(response.Content);
            var result = string.Format("Bearer {0}", json.First.First);

            // session.Session["oauth_token"] = result;
            return result;

            // }
        }
    }
}