using System.Collections.Generic;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Infrastructure.Constants
{
    public static class PaymentMethod
    {
        public static List<SagePaymentMethod> PaymentMethods = new List<SagePaymentMethod> {
            new SagePaymentMethod() { Method = "Check", Value= 0 },
            new SagePaymentMethod() { Method = "Cash", Value= 1 },
            new SagePaymentMethod() { Method = "Credit Card", Value = 2 },
            new SagePaymentMethod() { Method = "Bill Out", Value = 3 },
        };
    }
}