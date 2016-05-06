namespace BloomService.Domain.Entities.Concrete
{
    using System.Collections.Generic;

    using BloomService.Domain.Attributes;

    [CollectionName("ImageWorkOrderCollection")]
    public class SageImageWorkOrder : SageEntity
    {
        public List<string> Images { get; set; }

        public string WorkOrder { get; set; }
    }
}