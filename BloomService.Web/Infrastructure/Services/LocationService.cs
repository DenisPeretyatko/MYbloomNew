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

        public void ResolveLocation(SageLocation sageLocation)
        {
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
    }
}