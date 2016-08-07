using System.Linq;

namespace BloomService.Web.Infrastructure.Constants
{
    using Domain.Entities.Concrete;
    using System.Collections.Generic;

    public static class WorkOrderStatus
    {
        public static List<SageWorkOrderStatus> Status = new List <SageWorkOrderStatus> {
            new SageWorkOrderStatus { Status = Open, Value= OpenId },
            new SageWorkOrderStatus { Status = ReturnRequired, Value= ReturnRequiredId },
            new SageWorkOrderStatus { Status = WorkComplete, Value = WorkCompleteId },
            new SageWorkOrderStatus { Status = Closed, Value = ClosedId }
        };

        public const string Open = "Open";
        public const string ReturnRequired = "Return Required";
        public const string WorkComplete = "Work Complete";
        public const string Closed = "Closed";

        public const int OpenId = 0;
        public const int ReturnRequiredId = 1;
        public const int WorkCompleteId = 2;
        public const int ClosedId = 3;

        public static string ById(int status)
        {
            return Status.Single(x => x.Value == status).Status;
        }

        public static int ByStatus(string status)
        {
            return Status.Single(x => x.Status == status).Value;
        }
    }
}