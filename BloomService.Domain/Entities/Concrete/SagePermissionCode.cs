using BloomService.Domain.Attributes;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("PermissionCodeCollection")]
    public class SagePermissionCode : SageEntity
    {
        public int PERMISSIONCODE { get; set; }
        public string QINACTIVE { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
