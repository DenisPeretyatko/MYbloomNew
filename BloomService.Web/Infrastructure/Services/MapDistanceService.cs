using System;
using BloomService.Web.Infrastructure.Services.Interfaces;

namespace BloomService.Web.Infrastructure.Services
{
    public class MapDistanceService :IMapDistanceService
    {
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