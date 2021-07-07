using System;

namespace Soriana.PPS.Common.Extensions
{
    public static class DateTimeExtensions
    {
        #region Public Methods
        public static long ToUnixTime(this DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dateTime - epoch).TotalSeconds);
        }
        #endregion
    }
}
