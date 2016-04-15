using System.Collections.Generic;
using System.Web.Script.Serialization;
using BloomService.Web.Models;
using Microsoft.AspNet.SignalR;

namespace BloomService.Web.Infrastructure.Hubs
{
    public class LocationHub : Hub
    {
        public void GetLocations()
        {
            var json = JsonHelper.GetObjects("getNewLocations.json");
            var serializer = new JavaScriptSerializer();
            var list = serializer.ConvertToType<IEnumerable<LocationJsonModel>>(json);
            Clients.All.updateLocations(list);
        }
    }
}