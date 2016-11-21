using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static bool IsAfter(this DateTimeOffset me, DateTimeOffset before)
        {
            return me > before;
        }

        public static bool IsBefore(this DateTimeOffset me, DateTimeOffset after)
        {
            return me < after;
        }
    }
}
