using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloomService.Domain.Attributes;
using BloomService.Domain.Entities.Abstract;

namespace BloomService.Domain.Entities.Concrete
{
    [CollectionName("NotificationCollection")]
    public class Notification : SageEntity
    {
        public bool IsViewed { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
