using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// EditRuleTypes are used to validate the user input before saving the row value(s).
    /// </summary>
    public enum EditRuleType
    {
        /// <summary>
        /// This option is valid only in form editing module.
        /// By default the hidden fields are not editable. 
        /// If the field is hidden in the grid and edithidden is set, the field can be edited when add or edit methods are called.
        /// </summary>
        EditHidden,

        /// <summary>
        /// If set, the value will be checked and if empty, an error message will be displayed.
        /// </summary>
        Required,

        /// <summary>
        /// If set, the value will be checked and if this is not a number, an error message will be displayed.
        /// </summary>
        Number,

        /// <summary>
        /// If set, the value will be checked and if this is not a integer, an error message will be displayed.
        /// </summary>
        Integer,

        /// <summary>
        /// If set, the value will be checked and if the value is less than this, an error message will be displayed.
        /// </summary>
        MinValue,

        /// <summary>
        /// If set, the value will be checked and if the value is more than this, an error message will be displayed.
        /// </summary>
        MaxValue,

        /// <summary>
        /// If set, the value will be checked and if this is not valid e-mail, an error message will be displayed
        /// </summary>
        Email,

        /// <summary>
        /// If set, the value will be checked and if this is not valid url, an error message will be displayed
        /// </summary>
        Url,

        /// <summary>
        /// If set, a value from datefmt option is get (if not set ISO date is used) and the value will be checked
        /// and if this is not valid date, an error message will be displayed.
        /// </summary>
        Date,

        /// <summary>
        /// If set, the value will be checked and if this is not valid time, an error message will be displayed.
        /// Currently jqgrid supports only hh:mm format and optional am/pm at the end.
        /// </summary>
        Time,

        /// <summary>
        /// If set allows definition of custom checking rules via a custom function.
        /// </summary>
        Custom
    }

    public class EditRule
    {
        private static readonly Dictionary<EditRuleType, string> cRuleTypes = new Dictionary<EditRuleType, string>
            {
                {EditRuleType.EditHidden, "edithidden"}, 
                {EditRuleType.Required, "required"}, 
                {EditRuleType.Number, "number"}, 
                {EditRuleType.Integer, "integer"},
                {EditRuleType.MinValue, "minValue"},
                {EditRuleType.MaxValue, "maxValue"},
                {EditRuleType.Email, "email"},
                {EditRuleType.Url, "url"},
                {EditRuleType.Date, "date"},
                {EditRuleType.Time, "time"},
                {EditRuleType.Custom, "custom"}
            };

        public EditRule(EditRuleType pEditRuleType, string pValue)
        {
            this.Type = pEditRuleType;
            this.Value = pValue;
        }

        public EditRule(EditRuleType pEditRuleType)
        {
            this.Type = pEditRuleType;
        }

        public EditRuleType Type { get; private set; }
        public string Value { get; private set; }

        public string Render()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.Append(cRuleTypes[this.Type]);
            if (this.Type == EditRuleType.MaxValue
                || this.Type == EditRuleType.MinValue)
            {
                vStringBuilder.Append(this.Value);
                
            }
            else
            {
                vStringBuilder.Append(": true");                
            }

            if (this.Type == EditRuleType.Custom && !string.IsNullOrEmpty(this.Value))
            {
                vStringBuilder.Append(", custom_func: ");
                vStringBuilder.Append(this.Value);
            }
            return vStringBuilder.ToString();
        }
    }
}
