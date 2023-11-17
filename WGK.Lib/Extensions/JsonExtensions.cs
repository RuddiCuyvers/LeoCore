using System;
using System.Globalization;
using System.Linq;
//using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace WGK.Lib.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts a double to a json string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToJsonString(this double source)
        {
            return source.ToString(new CultureInfo("en-US"));
        }

        /// <summary>
        /// Converts a decimal to a json string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToJsonString(this decimal source)
        {
            return source.ToString(new CultureInfo("en-US"));
        }

        /// <summary>
        /// Converts a decimal? to a json string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToJsonString(this decimal? source)
        {
            return source.HasValue ? source.Value.ToString(new CultureInfo("en-US")) : "";
        }
    }
}
