using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WGK.Lib.Web.Mvc.Controllers
{
    public class BaseController : Controller
    {
        #region Constants

        private const string cInfoMessageKey = "INFO_MSG";

        /// <summary>
        /// Gets the key of the InformationMessage in de ViewData and TempData
        /// </summary>
        public static string InfoMessageKey
        {
            get { return cInfoMessageKey; }
        }

        private const string cErrorMessageKey = "ERROR_MSG";

        /// <summary>
        /// Gets the key of the ErrorMessage in de ViewData and TempData
        /// </summary>
        public static string ErrorMessageKey
        {
            get { return cErrorMessageKey; }
        }

        #endregion

        #region Constructors

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets, sets the information message that is displayed in the View
        /// </summary>
        public string InfoMessageForView
        {
            get { return this.ViewData[cInfoMessageKey] as string; }
            set { this.ViewData[cInfoMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the information message that is displayed in the redirected View
        /// </summary>
        public string InfoMessageForRedirect
        {
            get { return this.TempData[cInfoMessageKey] as string; }
            set { this.TempData[cInfoMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the Error message that is displayed in the View
        /// </summary>
        public string ErrorMessageForView
        {
            get { return this.ViewData[cErrorMessageKey] as string; }
            set { this.ViewData[cErrorMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the Error message that is displayed in the redirected View
        /// </summary>
        public string ErrorMessageForRedirect
        {
            get { return this.TempData[cErrorMessageKey] as string; }
            set { this.TempData[cErrorMessageKey] = value; }
        }

        #endregion
       
        #region Public Methods
        /// <summary>
        /// Gets the model state value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKey">The key.</param>
        /// <returns></returns>
     //   public T GetModelStateValue<T>(string pKey)
      //  {
        //    ModelStateEntry vModelStateItem;
     //       if (this.ModelState.TryGetValue(pKey, out vModelStateItem) && (vModelStateItem.RawValue != null))
        //    {
       //         return (T)vModelStateItem.RawValue.ConvertTo(typeof(T), null);
      //      }
      //      return default(T);
      //  }

        /// <summary>
        /// Gets the form collection value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pFormCollection">The form collection.</param>
        /// <param name="pKey">The key.</param>
        /// <returns></returns>
        public T GetFormCollectionValue<T>(FormCollection pFormCollection, string pKey)
        {
            // TODO This code has not been tested!!!

            string vFormValue = pFormCollection[pKey];

            //if (typeof(T) == typeof(string))
            //{
            //    return vFormValue;
            //}

            if (!string.IsNullOrEmpty(vFormValue))
            {
                TypeConverter vConverter = TypeDescriptor.GetConverter(typeof(T));
                if (vConverter.CanConvertFrom(typeof(string)))
                {
                    return (T)vConverter.ConvertFrom(vFormValue);
                }
                TypeConverter vToConverter = TypeDescriptor.GetConverter(typeof(string));
                return (T)vToConverter.ConvertTo(vFormValue, typeof(T));
            }
            return default(T);
        }
        #endregion
        
        #region Protected Methods
       
        #endregion
    }
}