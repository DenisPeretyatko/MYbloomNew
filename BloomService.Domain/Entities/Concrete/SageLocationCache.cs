using System;
using BloomService.Domain.Attributes;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("LocationCacheCollection")]
    public class SageLocationCache : SageEntity
    {
        public long Location { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
