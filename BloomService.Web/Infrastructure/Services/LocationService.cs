namespace BloomService.Web.Services.Concrete
{
    using System.Collections.Generic;
    using System.Net;

    using Domain.Entities.Concrete;
    using Abstract;
    using Domain.Repositories.Abstract;
    using MongoDB.Bson;
    using System.Linq;
    using Domain.Entities.Concrete.MessageResponse;
    using System.Text;
    using System.Xml.Serialization;
    using System.IO;
    using System.Threading;

    public class LocationService : ILocationService
    {
        private static readonly string url = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

        private List<SageWorkOrder> workOrders;
        private readonly IRepository _repository;

        public LocationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool ResolveLocation(SageLocation entity)
        {
            if (entity.Id == null)
                entity.Id = ObjectId.GenerateNewId().ToString();
            if (workOrders == null)
                workOrders = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();

            if (workOrders.Any(x => x.Location == entity.Name))
            {
                Thread.Sleep(100);
                var parametersSearch = entity.Address + " " + entity.City + " " + entity.ZIP + " " + entity.State;
                var location = GetLocation(parametersSearch);
                if (location != null && location.result != null && location.result.Any())
                {
                    var geometry = location.result.FirstOrDefault().geometry;
                    if (geometry != null && geometry.location != null)
                    {
                        entity.Latitude = geometry.location.lat;
                        entity.Longitude = geometry.location.lng;
                    }
                }
            }
            return _repository.Update(entity);
        }


        public GeocodeResponse GetLocation(string address)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            var searchUrl = string.Format(url, address);
            var request = HttpWebRequest.Create(searchUrl);
            request.Method = "POST";
            var newStream = request.GetRequestStream();
            newStream.Close();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var dataStream = response.GetResponseStream();
                var serializer = new XmlSerializer(typeof(GeocodeResponse));
                object model;
                using (var reader = new StreamReader(dataStream))
                {
                    model = serializer.Deserialize(reader);
                }
                return (GeocodeResponse)model;
            }
            catch
            {
                return null;
            }
        }
    }
}