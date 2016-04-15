using System.Collections.Generic;

namespace BloomService.Web.Infrastructure.Constants
{
    public static class RateSheets
    {
        public static readonly string StandardCallRate = "Standard Call Rate";
        public static readonly string TripCharge = "Trip Charge";
        public static readonly string WarrantyCallNoCharge = "Warranty Call - No Charge";
        public static readonly string RoofingConsult = "Roofing Consult";
        public static readonly string MACI = "MACI";
        public static readonly string NonWarrantyNoCharge = "Non-Warranty / No Charge";
        public static readonly string DetroitRenewablePower = "Detroit Renewable Power";
        public static readonly string Midlink = "Midlink";
        public static readonly string SnowRemoval = "Snow Removal";
        public static readonly string PromotionalTwooMenHalfDay = "Promotional - 2 men 1/2 day";

        public static List<string> RateSheetsList = new List<string>
            {
                StandardCallRate,
                StandardCallRate,
                TripCharge,
                WarrantyCallNoCharge,
                RoofingConsult,
                MACI,
                NonWarrantyNoCharge,
                DetroitRenewablePower,
                Midlink,
                SnowRemoval,
                PromotionalTwooMenHalfDay
            };
    }
}