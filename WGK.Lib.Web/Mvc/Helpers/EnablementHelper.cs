using System.Collections.Generic;

using WGK.Lib.Web.Enumerations;

namespace WGK.Lib.Web.Mvc.Helpers
{
    internal static class EnablementHelper
    {
        #region HtmlAttributes Extension methods
        /// <summary>
        /// Adds "readonly" to the HtmlAttributes if needed by status enablement
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns>true if the ReadOnly attribute was added</returns>
        public static bool AddReadOnlyAttributeIfNeeded(
            this IDictionary<string, object> pHtmlAttributes,
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            if (IsReadOnly(
                pActivityStatus,
                pActivityStatusEnablement,
                pAuthorizationEnablement,
                pCustomEnablementProvider,
                pCustomEnablementKey))
            {
                if (!pHtmlAttributes.ContainsKey("readOnly"))
                {
                    pHtmlAttributes.Add("readOnly", "read-only");
                }
                else
                {
                    pHtmlAttributes["readOnly"] = "read-only";
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds "disabled" to the HtmlAttributes if needed by status enablement 
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns>true if the Disabled attribute was added</returns>
        public static bool AddDisabledAttributeIfNeeded(
            this IDictionary<string, object> pHtmlAttributes,
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            if (IsReadOnly(
                pActivityStatus,
                pActivityStatusEnablement,
                pAuthorizationEnablement,
                pCustomEnablementProvider,
                pCustomEnablementKey))
            {
                if (!pHtmlAttributes.ContainsKey("disabled"))
                {
                    pHtmlAttributes.Add("disabled", "disabled");
                }
                else
                {
                    pHtmlAttributes["disabled"] = "disabled";
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds "enabled=false" to the HtmlAttributes if needed by status enablement
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns>true if the EnabledFalse attribute was added</returns>
        public static bool AddEnabledFalseAttributeIfNeeded(
            this IDictionary<string, object> pHtmlAttributes,
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            if (IsReadOnly(
                pActivityStatus,
                pActivityStatusEnablement,
                pAuthorizationEnablement,
                pCustomEnablementProvider,
                pCustomEnablementKey))
            {
                if (!pHtmlAttributes.ContainsKey("enabled"))
                {
                    pHtmlAttributes.Add("enabled", "false");
                }
                else
                {
                    pHtmlAttributes["enabled"] = "false";
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a light shade of gray color to the HtmlAttributes if needed by status enablement
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns>true if the EnabledFalse attribute was added</returns>
        public static bool AddGrayedOutAttributeIfNeeded(
            this IDictionary<string, object> pHtmlAttributes,
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            if (IsReadOnly(
                pActivityStatus,
                pActivityStatusEnablement,
                pAuthorizationEnablement,
                pCustomEnablementProvider,
                pCustomEnablementKey))
            {
                const string cGrayedOutStyle = "color:lightgrey;";

                if (!pHtmlAttributes.ContainsKey("style"))
                {
                    pHtmlAttributes.Add("style", cGrayedOutStyle);                   
                }
                else
                {
                    pHtmlAttributes["style"] = pHtmlAttributes["style"] + ";" + cGrayedOutStyle;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a 35% opacity to the HtmlAttributes if needed by status enablement
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns>true if the EnabledFalse attribute was added</returns>
        public static bool AddOpacityAttributeIfNeeded(
            this IDictionary<string, object> pHtmlAttributes,
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            if (IsReadOnly(
                pActivityStatus,
                pActivityStatusEnablement,
                pAuthorizationEnablement,
                pCustomEnablementProvider,
                pCustomEnablementKey))
            {
                const string cOpacityStyle = "opacity:0.35;";

                if (!pHtmlAttributes.ContainsKey("style"))
                {
                    pHtmlAttributes.Add("style", cOpacityStyle);
                }
                else
                {
                    pHtmlAttributes["style"] = pHtmlAttributes["style"] + ";" + cOpacityStyle;
                }
                return true;
            }
            return false;
        }
        #endregion
        
        #region Public Methods Enablement/Visibility
        /// <summary>
        /// Check if status enablement requires widgets to be read-only or disabled
        /// </summary>
        /// <param name="pActivityStatusEnablement"></param>
        /// <param name="pActivityStatus"> </param>
        /// <param name="pAuthorizationEnablement"> </param>
        /// <param name="pCustomEnablementProvider"></param>
        /// <param name="pCustomEnablementKey"></param>
        /// <returns></returns>
        public static bool IsReadOnly(
            ActivityStatusEnum pActivityStatus,
            ActivityStatusEnablementEnum pActivityStatusEnablement,
            AuthorizationEnablement pAuthorizationEnablement,
            ICustomEnablementProvider pCustomEnablementProvider,
            string pCustomEnablementKey)
        {
            // -- ActivityStatus enablement
            if (pActivityStatusEnablement == ActivityStatusEnablementEnum.ReadOnly)
            {
                // The widget is not enabled in any ActivitySatus
                // Remark: ReadOnly is the default value (i.e., secure by default)
                return true;
            }

            // Ignore the View's ActivityStatus if the widget's ActivityStatusEnablement is set to Ignore
            if (pActivityStatusEnablement != ActivityStatusEnablementEnum.Ignore)
            {
                // Disable widget if the specified ActivityStatusEnablement has no flag corresponding to the View's  current ActivityStatus.
                switch (pActivityStatus)
                {
                    case ActivityStatusEnum.Select:
                        if (!pActivityStatusEnablement.HasFlag(ActivityStatusEnablementEnum.SelectOnly))
                        {
                            return true;
                        }
                        break;

                    case ActivityStatusEnum.Insert:
                        if (!pActivityStatusEnablement.HasFlag(ActivityStatusEnablementEnum.InsertOnly))
                        {
                            return true;
                        }
                        break;

                    case ActivityStatusEnum.View:
                        if (!pActivityStatusEnablement.HasFlag(ActivityStatusEnablementEnum.ViewOnly))
                        {
                            return true;
                        }
                        break;

                    case ActivityStatusEnum.Edit:
                        if (!pActivityStatusEnablement.HasFlag(ActivityStatusEnablementEnum.EditOnly))
                        {
                            return true;
                        }
                        break;

                    // DELETED - We probably don't need a search results modus
                    //case ActivityStatusEnum.Search:
                    //    if (!pActivityStatusEnablement.HasFlag(ActivityStatusEnablementEnum.SearchOnly))
                    //    {
                    //        return true;
                    //    }
                    //    break;

                    default: // ActivityStatusEnum.None
                        // Disable the widget if we cannot determine the View's ActivityStatus (i.e., secure by default)
                        return true;
                }                
            }

            // -- Authorization enablement
            if (pAuthorizationEnablement != null)
            {
                if (pAuthorizationEnablement.IsReadOnly())
                {
                    // Disable the widget if user has none of the required permission(s) on task(s)
                    return true;                                                                               
                }              
            }

            // -- Custom enablement
            if (!string.IsNullOrEmpty(pCustomEnablementKey))
            {
                if (pCustomEnablementProvider == null)
                {
                    // Disable the widget if there is no CustomEnablementProvider (i.e., secure by default)
                    return true;
                }
                if (pCustomEnablementProvider.IsReadOnly(pCustomEnablementKey))
                {
                    // Disable the widget if required by the custom enablement logic
                    return true;
                }
            }

            // If we get here it is not needed to disable the widget ...
            return false;
        }
        #endregion
    }
}