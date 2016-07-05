namespace BloomService.Web.Infrastructure.Constants
{
    using Models;
    using System.Collections.Generic;

    public static class PaymentMethod
    {
        public static List<PaymentMethodModel> PaymentMethods = new List<PaymentMethodModel> {
            new PaymentMethodModel() { Method = "Check", Value= 0 },
            new PaymentMethodModel() { Method = "Cash", Value= 1 },
            new PaymentMethodModel() { Method = "Credit card", Value = 2 },
            new PaymentMethodModel() { Method = "Bill out", Value = 3 },
        };
    }
}