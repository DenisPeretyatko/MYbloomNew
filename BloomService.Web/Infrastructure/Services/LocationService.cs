namespace BloomService.Web.Services.Concrete
{
    using System.Net;

    using Domain.Entities.Concrete;
    using Abstract;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.IO;
    using System.Threading;
    using Sage.WebApi.Infratructure.MessageResponse;
    public class LocationService : ILocationService
    {
        private static readonly string url = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

        public void ResolveLocation(SageLocation sageLocation)
        {
            Thread.Sleep(1000);
            var parametersSearch = sageLocation.Address + " " + sageLocation.City + " " + sageLocation.ZIP + " " + sageLocation.State;
            var location = GetLocation(parametersSearch);
            if (location != null && location.result != null && location.result.Any())
            {
                var geometry = location.result.FirstOrDefault().geometry;
                if (geometry != null && geometry.location != null)
                {
                    sageLocation.Latitude = geometry.location.lat;
                    sageLocation.Longitude = geometry.location.lng;
                }
            }
        }


        public GeocodeResponse GetLocation(string address)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
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
    }
}