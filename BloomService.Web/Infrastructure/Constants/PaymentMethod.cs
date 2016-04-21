using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloomService.Web.Infrastructure.Constants
{
    public static class PaymentMethod
    {
        public static readonly string Check = "Check";
        public static readonly string BillOut = "Bill Out";
        public static readonly string CreditCard = "Credit Card";
        public static readonly string Cash = "Cash";

        public static List<string> PaymentMethodList = new List<string>
            {
                Check,
                BillOut,
                CreditCard,
                Cash
            };
    }
}