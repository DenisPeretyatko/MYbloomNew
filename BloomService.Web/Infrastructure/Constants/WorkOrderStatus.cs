using System.Collections.Generic;
using System.Linq;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Infrastructure.Constants
{
    public static class WorkOrderStatus
    {
        public static List<SageWorkOrderStatus> Status = new List<SageWorkOrderStatus> {
            new SageWorkOrderStatus { Status = Open, Value= OpenId },
            new SageWorkOrderStatus { Status = ReturnRequired, Value= ReturnRequiredId },
            new SageWorkOrderStatus { Status = WorkComplete, Value = WorkCompleteId },
            new SageWorkOrderStatus { Status = Closed, Value = ClosedId },
            new SageWorkOrderStatus { Status = Cancelled, Value = CancelledId },
            new SageWorkOrderStatus { Status = ExpiredQuote, Value = ExpiredQuoteId },
            new SageWorkOrderStatus { Status = Reviewed, Value = ReviewedId },
            new SageWorkOrderStatus { Status = Invoiced, Value = InvoicedId }
        };
        public static List<SageWorkOrderStatus> StatusForManager = new List<SageWorkOrderStatus> {
            new SageWorkOrderStatus { Status = Open, Value= OpenId },
            new SageWorkOrderStatus { Status = ReturnRequired, Value= ReturnRequiredId },
            new SageWorkOrderStatus { Status = WorkComplete, Value = WorkCompleteId },
            //new SageWorkOrderStatus { Status = Closed, Value = ClosedId },
            new SageWorkOrderStatus { Status = Cancelled, Value = CancelledId },
            new SageWorkOrderStatus { Status = Reviewed, Value = ReviewedId }
        };

        public const string Open = "Open";
        public const string ReturnRequired = "Return Required";
        public const string WorkComplete = "Work Complete";
        public const string Closed = "Closed";
        public const string Cancelled = "Cancelled";
        public const string ExpiredQuote = "Expired Quote";
        public const string Reviewed = "Reviewed";
        public const string Invoiced = "Invoiced";

        public const int OpenId = 0;
        public const int ReturnRequiredId = 1;
        public const int WorkCompleteId = 2;
        public const int ClosedId = 3;
        public const int CancelledId = 4;
        public const int ExpiredQuoteId = 5;
        public const int ReviewedId = 6;
        public const int InvoicedId = 7;

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