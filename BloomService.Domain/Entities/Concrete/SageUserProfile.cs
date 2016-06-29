using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloomService.Domain.Attributes;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("NotificationTimeCollection")]
    public class SageUserProfile : SageEntity
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
