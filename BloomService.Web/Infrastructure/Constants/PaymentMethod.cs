using System.ComponentModel.DataAnnotations;

namespace BloomService.Web.Infrastructure.Constants
{
    public static class PaymentMethod
    {
        public static readonly string Cash = "Cash";
        public static readonly string WireTransfer = "Wire Transfer";
    }
}