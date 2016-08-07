namespace BloomService.Web.Infrastructure.Constants
{
    using Domain.Entities.Concrete;
    using System.Collections.Generic;

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