using System;

namespace WGK.Lib.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the last datetime value of the specified day
        /// e.g. LastDateTimeOfDay(2004/02/14) = 2004/02/14 23:59:59.998
        /// milliseconds 998 and not 999 as SQL Server DB rounds 999 to the next second: 998 is rounded to 997
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public static DateTime LastDateTimeOfDay(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, 23, 59, 59, 998);
        }

        /// <remarks>
        /// Returns the beginning datetime value of the specified day
        /// e.g. FirstDateTimeOfDay(2004/02/14) = 2004/02/14 00:00:00.000
        /// </remarks>
        /// <param name="pDateTime">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime FirstDateTimeOfDay(this DateTime pDateTime)
        {
            return pDateTime.Date;
        }

        /// <remarks>
        /// Returns the beginning datetime value of the specified day
        /// e.g. NoonDateTimeOfDay(2004/02/14) = 2004/02/14 12:00:00.000
        /// </remarks>
        /// <param name="pDateTime">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime NoonDateTimeOfDay(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, 12, 0, 0, 0);
        }

        /// <summary>
        /// Gets the first day of week.
        /// </summary>
        /// <param name="pDate">The date.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime pDate)
        {
            return pDate.AddDays(-pDate.DayOfWeek.ToInt() + 1);
        }

        /// <summary>
        /// Gets the last day of week.
        /// </summary>
        /// <param name="pDate">The date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfWeek(this DateTime pDate)
        {
            return pDate.AddDays(-pDate.DayOfWeek.ToInt() + 5);
        }

        /// <summary>
        /// Gets the first day of month.
        /// </summary>
        /// <param name="pDate">The date.</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime pDate)
        {
            return new DateTime(pDate.Year, pDate.Month, 1, pDate.Hour, pDate.Minute, pDate.Second, pDate.Millisecond);
        }

        /// <summary>
        /// Gets the last day of month.
        /// </summary>
        /// <param name="pDate">The date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime pDate)
        {
            return
                new DateTime(pDate.Month == 12 ? pDate.Year + 1 : pDate.Year, pDate.Month == 12 ? 1 : pDate.Month + 1, 1,
                    pDate.Hour, pDate.Minute, pDate.Second, pDate.Millisecond).AddDays(-1);
        }

        /// <summary>
        /// Calculates the difference in months between this instance and the specified reference date
        /// </summary>
        /// <example>
        /// If CompareDay is true:
        /// If the instance date is 1/7/2010 and the reference date is 1/1/2010  the number of months is 6 
        /// If the instance date is 2/7/2010 and the reference date is 1/1/2010  the number of months should be counted as 7
        /// </example>
        /// <param name="pDate"></param>
        /// <param name="pReferenceDate"></param>
        /// <param name="pCompareDay">If true then take day value into account for calculating the number of months</param>
        /// <returns></returns>
        public static int DifferenceInMonths(this DateTime pDate, DateTime pReferenceDate, bool pCompareDay = false)
        {
            int vMonths = (pDate.Year - pReferenceDate.Year) * 12 + (pDate.Month - pReferenceDate.Month);
            if (pCompareDay && pDate.Day > pReferenceDate.Day)
            {
                vMonths += 1;
            }
            return vMonths;
        }

        /// <summary>
        /// Calculates the difference in days between this instance and the specified date
        /// </summary>
        /// <param name="pDate"></param>
        /// <param name="pCompareDate"></param>
        /// <returns></returns>
        public static int DifferenceInDay(this DateTime pDate, DateTime pCompareDate)
        {
            return pDate.Subtract(pCompareDate).Days;
        }

        /// <summary>
        /// Toes the int.
        /// </summary>
        /// <param name="pDayOfWeek">The day of week.</param>
        /// <returns></returns>
        public static int ToInt(this DayOfWeek pDayOfWeek)
        {
            int vResult = 0;

            switch (pDayOfWeek)
            {
                case DayOfWeek.Monday:
                    vResult = 1;
                    break;
                case DayOfWeek.Tuesday:
                    vResult = 2;
                    break;
                case DayOfWeek.Wednesday:
                    vResult = 3;
                    break;
                case DayOfWeek.Thursday:
                    vResult = 4;
                    break;
                case DayOfWeek.Friday:
                    vResult = 5;
                    break;
                case DayOfWeek.Saturday:
                    vResult = 6;
                    break;
                case DayOfWeek.Sunday:
                    vResult = 7;
                    break;
            }

            return vResult;
        }

        /// <summary>
        /// Parses the time string in hh:mm format to decimal hours value, 
        /// optionally rounding the value to a number of decimal places.
        /// </summary>
        /// <param name="pTimeString">The time string.</param>
        /// <param name="pDecimals">The number of decimal places in the return value.</param>
        /// <returns></returns>
        public static decimal ParseTimeString(this string pTimeString, int? pDecimals)
        {
            TimeSpan vTimeSpan;
            if (TimeSpan.TryParse(pTimeString, out vTimeSpan))
            {
                // Perform calculation in decimal type
                decimal vFraction = (decimal)vTimeSpan.Minutes / 60;
                if (pDecimals != null)
                {
                    vFraction = Math.Round(vFraction, pDecimals.Value);
                }
                return vTimeSpan.Hours + vFraction;
            }
            return decimal.Zero;
        }

        /// <summary>
        /// Converts nullable decimal to time string in the format "hh:mm" (hours and minutes).
        /// Seconds are rounded to the nearest minute. Days are added to the hours.
        /// </summary>
        /// <param name="pTime">The time.</param>
        /// <returns></returns>
        public static string ToTimeString(this decimal? pTime)
        {
            if (!pTime.HasValue)
            {
                return "00:00";
            }

            return ToTimeString(pTime.Value);
        }

        /// <summary>
        /// Converts decimal to time string in the format "hh:mm" (hours and minutes).
        /// Seconds are rounded to the nearest minute. Days are added to the hours.
        /// </summary>
        /// <param name="pTime">The time.</param>
        /// <returns></returns>
        public static string ToTimeString(this decimal pTime)
        {
            TimeSpan vTimeSpan = TimeSpan.FromHours((double)pTime);
            return string.Format(
                "{0:D2}:{1:D2}",
                vTimeSpan.Hours + (vTimeSpan.Days * 24),
                vTimeSpan.Minutes + (vTimeSpan.Seconds >= 30 ? 1 : 0));
        }
    }
}