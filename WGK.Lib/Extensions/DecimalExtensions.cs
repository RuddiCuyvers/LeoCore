using System;

namespace WGK.Lib.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Converts decimal to long.
        /// </summary>
        public static long ToLong(this decimal pDecimal)
        {
            return (long)Math.Round(pDecimal, 0);
        }

        /// <summary>
        /// Converts nullable decimal to long.
        /// </summary>
        public static long? ToNullableLong(this decimal? pDecimal)
        {
            return pDecimal == null
                ? null
                : (long?)Math.Round(pDecimal.Value, 0);
        }

        /// <summary>
        /// Converts decimal to int.
        /// </summary>
        public static int ToInt(this decimal pDecimal)
        {
            return (int)Math.Round(pDecimal, 0);
        }

        /// <summary>
        /// Converts nullable decimal to int.
        /// </summary>
        public static int? ToNullableInt(this decimal? pDecimal)
        {
            return pDecimal == null
                ? null
                : (int?)Math.Round(pDecimal.Value, 0);
        }

        /// <summary>
        /// Converts decimal to short.
        /// </summary>
        public static short ToShort(this decimal pDecimal)
        {
            return (short)Math.Round(pDecimal, 0);
        }

        /// <summary>
        /// Converts nullable decimal to short.
        /// </summary>
        public static short? ToNullableShort(this decimal? pDecimal)
        {
            return pDecimal == null
                ? null
                : (short?)Math.Round(pDecimal.Value, 0);
        }
    }
}