using System.Globalization;
using Newtonsoft.Json;


namespace WGK.Lib.Web.Globalization
{
    /// <summary>
    /// Server-side class for initializing client-side Sys.CultureInfo.CurrentCulture JavaScript class
    /// </summary>
    /// <remarks>
    /// See:
    /// http://www.chrispoulter.com/development-blog/localized-currencies-and-decimals-using-microsoftajax-js
    /// </remarks>
    public class ClientCultureInfo
    {
        public string name;
        public DateTimeFormatInfo dateTimeFormat;
        public NumberFormatInfo numberFormat;

        public ClientCultureInfo(CultureInfo cultureInfo)
        {
            this.name = cultureInfo.Name;
            this.numberFormat = cultureInfo.NumberFormat;
            this.dateTimeFormat = cultureInfo.DateTimeFormat;
        }

       
    }
}
