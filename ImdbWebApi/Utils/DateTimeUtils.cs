using System.Globalization;
using System;
using ImdbWebApi.Exceptions;

namespace ImdbWebApi.Utils
{
    public class DateTimeUtils
    {
        public static DateTime ParseFromYMD(string date)
        {
            try
            {
                DateTime result = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return result;
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid date format.");
            }
        }
    }
}
