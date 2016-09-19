using System;
using BloomService.Web.Infrastructure.Dependecy;
using BloomService.Web.Infrastructure.Mongo;

namespace BloomService.Web.Infrastructure.Services
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml.Serialization;

    using BloomService.Domain.Entities.Concrete;
    using BloomService.Web.Infrastructure.Services.Interfaces;
    using BloomService.Web.Models;

    public class LocationService : ILocationService
    {
        private static readonly string url = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";
        private readonly IRepository _repository;

        public LocationService()
        {
            _repository = ComponentContainer.Current.Get<IRepository>();
        }

        public void ResolveLocation(SageLocation sageLocation, SageLocationCache cache = null)
        {
            //var sageLocationCache = new SageLocationCache();
            Thread.Sleep(1000);
            var parametersSearch = GetGeoLocationAddress(sageLocation);
            var location = GetLocation(parametersSearch);
            if (location?.result != null && location.result.Any())
            {
                var geometry = location.result.First().geometry;
                if (geometry?.location != null)
                {
                    sageLocation.Latitude = geometry.location.lat;
                    sageLocation.Longitude = geometry.location.lng;

                    var sageLocationCache = cache ?? _repository.SearchFor<SageLocationCache>(x => x.Location == sageLocation.Location)
                        .SingleOrDefault();


                    if (sageLocationCache != null)
                    {
                        sageLocationCache.Latitude = geometry.location.lat;
                        sageLocationCache.Longitude = geometry.location.lng;
                        sageLocationCache.Location = sageLocation.Location;
                        sageLocationCache.Address = sageLocation.Address;
                        sageLocationCache.ResolvedDate = DateTime.UtcNow;
                        _repository.Update(sageLocationCache);
                    }
                    else
                    {
                        _repository.Add(new SageLocationCache
                        {
                            Latitude = geometry.location.lat,
                            Longitude = geometry.location.lng,
                            Location = sageLocation.Location,
                            Address = sageLocation.Address,
                            ResolvedDate = DateTime.UtcNow
                        });
                    }
                }
            }
        }

        public GeocodeResponse GetLocation(string address)
        {
            var searchUrl = string.Format(url, address);
            var request = WebRequest.Create(searchUrl);
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

        private string GetGeoLocationAddress(SageLocation sageLocation)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(sageLocation.Address))
                sb.AppendFormat("{0} ", sageLocation.Address);
            if (!string.IsNullOrEmpty(sageLocation.Address2) && string.IsNullOrEmpty(sageLocation.Address))
                sb.AppendFormat("{0} ", sageLocation.Address2);

            sb.AppendFormat("{0} ", sageLocation.City);
            sb.AppendFormat("{0} ", sageLocation.ZIP);
            sb.AppendFormat("{0} ", sageLocation.State);

            return sb.ToString().TrimEnd(' ');
        }



        public double Distance(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2)
        {
            const int R = 6378137;
            var dLat = Rad((double)latitude2 - (double)latitude1);
            var dLong = Rad((double)longitude2 - (double)longitude1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Rad((double)latitude1)) * Math.Cos(Rad((double)latitude2)) *
              Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d * 0.000621371;
        }

        private double Rad(double x)
        {
            return x * Math.PI / 180;
        }
    }
}