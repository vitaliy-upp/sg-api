using System;

namespace PaymentStripe.Utils
{
    public static class StripeUtils
    {
        public static long PriceToUnits(decimal price)
        {
            return Convert.ToInt64(price * 100);
        }

        public static decimal UnitsToPrice(long units)
        {
            return Convert.ToDecimal(units / 100);
        }
    }
}
