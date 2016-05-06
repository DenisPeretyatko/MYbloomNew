using BloomService.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BloomService.Domain.Entities
{
    [CollectionName("ImageWorkOrderCollection")]
    public class ImageWorkOrder : SageEntity
    {
        public string WorkOrder { get; set; }
        public List<string> Images { get; set; }
    }
}
