using BloomService.Domain.Entities.Concrete.MessageResponse;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace BloomService.Domain.Services
{
    public static class GoogleApi
    {
        private static readonly string url = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

        public static GeocodeResponse GetLocation(string address)
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
