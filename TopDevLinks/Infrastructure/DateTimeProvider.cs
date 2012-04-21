using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Infrastructure
{
    public static class DateTimeProvider
    {
        private static DateTime? dateTimeToReturn;

        public static DateTime Now
        {
            get { return dateTimeToReturn == null ? DateTime.Now : dateTimeToReturn.Value; }
        }

        public static void SetDateTimeToReturn(DateTime overriddenCurrentDateTime)
        {
            dateTimeToReturn = overriddenCurrentDateTime;
        }

        public static void ResetCurrentDateTime()
        {
            dateTimeToReturn = null;
        }
    }
}