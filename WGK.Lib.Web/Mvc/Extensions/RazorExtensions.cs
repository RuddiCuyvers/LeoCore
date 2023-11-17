using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WGK.Lib.Web.Mvc.Extensions
{
    /// <summary>
    /// Static class containing extensions for the Razor View Engine
    /// </summary>
    public static class RazorExtensions
    {
        /// <summary>
        /// IEnumerable extension method that renders all elements in a collection using a templated Razor delegate to
        /// specify the rendered html. 
        /// </summary>
        /// <typeparam name="T">Type of items in the IEnumerable collection</typeparam>
        /// <param name="pEnumerable">IEnumerable collection of items that must be rendered</param>
        /// <param name="pRazorTemplate">Templated Razor delegate specifying the html to render</param>
        /// <returns></returns>
        /// <example> Render items from an array. Note that templated Razor delegates use a single @item parameter.
        /// @{
        ///    var vComics = new[] { 
        ///        new ComicBook {Title = "Groo", Publisher = "Dark Horse Comics"},
        ///        new ComicBook {Title = "Spiderman", Publisher = "Marvel"}
        ///    };
        /// }
        ///<table>
        ///@vComics.Repeat(
        ///  @<tr>
        ///    <td>@item.Title</td>
        ///    <td>@item.Publisher</td>
        ///  </tr>)
        ///</table>
        /// </example>
       // public static HelperResult Repeat<T>(this IEnumerable<T> pEnumerable, Func<T, HelperResult> pRazorTemplate)
      //  {
          //  return new HelperResult(pTextWriter =>
          //  {
             //   foreach (var vItem in pEnumerable)
            //    {
                 //   pRazorTemplate(vItem).WriteAction(pTextWriter);
               // }
           // });
      // }
    }
}
