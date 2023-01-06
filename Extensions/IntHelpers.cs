using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Extensions
{
    public static class IntHelpers
    {
        public static bool HasWholeRoot(this int number)
        {
            int root = (int)Math.Sqrt(number);
            return number == root * root;
        }
    }
}