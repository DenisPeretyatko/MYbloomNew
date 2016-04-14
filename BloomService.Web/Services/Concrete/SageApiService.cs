using BloomService.Domain.Entities;
using BloomService.Domain.UnitOfWork;
using BloomService.Web.Services;
using BloomService.Web.Utils;
using MongoDB.Bson;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace BloomService.Web.Managers
{
    public class SageApiService<TEntity> : ISageApiService<TEntity> where TEntity : class, IEntity
    {
        private IRestClient restClient;
        private ISession session;
        private IUnitOfWork unitOfWork;

        public string EndPoint { get; set; }

        public SageApiService(IRestClient restClient, IUnitOfWork unitOfWork)
        {
            this.restClient = restClient;
            this.unitOfWork = unitOfWork;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var items = unitOfWork.GetEntities<TEntity>().GetAll().Take(20).ToArray();

            if (items.Any())
            {
                return items;
            }

            var request = new RestRequest(EndPoint, Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetAuthToken()));
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
            var item = unitOfWork.GetEntities<TEntity>().GetById(ObjectId.Parse(id));

            if (item != null)
            {
                return item;
            }

            var request = new RestRequest(EndPoint, Method.GET);
            request.AddUrlSegment("id", id);
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetAuthToken()));
            var response = restClient.Execute(request);
            var result = response.Content as TEntity;

            unitOfWork.GetEntities<TEntity>().Insert(result);

            return result;
        }

        public virtual IEnumerable<TEntity> Add(Properties properties)
        {
            var request = new RestRequest(EndPoint, Method.POST);
            request.AddObject(properties);
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetAuthToken()));
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        public virtual IEnumerable<TEntity> Edit(Properties properties)
        {
            var request = new RestRequest(EndPoint, Method.PUT);
            request.AddObject(properties);
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetAuthToken()));
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }


        public virtual IEnumerable<TEntity> Delete(Properties properties)
        {
            var request = new RestRequest(EndPoint, Method.DELETE);
            request.AddObject(properties);
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetAuthToken()));
            var response = restClient.Execute<List<TEntity>>(request);
            var result = response.Data;
            return result;
        }

        protected string GetAuthToken()
        {
            //if(session.Session["oauth_token"] != null)
            //{
            //    return session.Session["oauth_token"].ToString();
            //}
            //else
            //{
            var username = System.Configuration.ConfigurationManager.AppSettings["SageUsername"];
            var password = System.Configuration.ConfigurationManager.AppSettings["SagePassword"];

            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            var response = restClient.Execute(request);
            var json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
            var result = json.First.First.ToString();

            //session.Session["oauth_token"] = result;

            return result;
            //}
        }
    }
}