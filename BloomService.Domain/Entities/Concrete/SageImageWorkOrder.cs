namespace BloomService.Domain.Entities.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Attributes;

    [CollectionName("ImageWorkOrderCollection")]
    public class SageImageWorkOrder : SageEntity
    {
        public List<ImageLocation> Images { get; set; }

        public long WorkOrder { get; set; }
        public string WorkOrderBsonId { get; set; }
    }

    public class ImageLocation
    {
        public string Image { get; set; }

        public string BigImage { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }
    }
}