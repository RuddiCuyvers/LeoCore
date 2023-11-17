using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WGK.Lib.Web.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WGK.Lib.Web.Mvc.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class NumericUpDownControl
    {
        /*
        jQuery UI Numeric Up/Down v1.4.1
        https://github.com/flamewave/jquery-ui-numeric 
        
        This widget allows you to turn any text or number input box (input[type=text],input[type=number]) into a numeric
        "up/down" picker (otherwise known as a "spinner"). Users can use keyboard arrow keys or page up/down keys to adjust
        the number, or they can click the up/down buttons that are added to the right of the input.

        You are also able to format the way the number is displayed in the input so that a specific numeric format is enforced.
        This is done through the use of the $.numberFormat() function included with this widget.  
 
        ---------------------------------------------
        Available Options (and their default values):
        ---------------------------------------------

        disabled: false
            Indicates if the widget is disabled.

        keyboard: true
            Indicates if keyboard keys should be allowed to increment/decrement the input value.

        showCurrency: false
            A value indicating if the currency symbol should be displayed to the left of the input.

        currencySymbol: null
            The currency symbol to use.

        title: null
            The tool tip text of the input.

        buttons: true
            Indicates if the up/down buttons should be displayed to the right of the input.

        upButtonIcon: 'ui-icon-triangle-1-n'
            Icon of the up button.

        upButtonTitle: null
            Tooltip text of the up button.

        downButtonIcon: 'ui-icon-triangle-1-s'
            Icon of the down button.

        downButtonTitle: null
            Tooltip text of the down button.

        emptyValue: 0
            If the value equals the value specified by this option, then the input is made "empty" so that no value is visible.
            To disable this functionality, set this option to false.

        minValue: false
            The minimum value allowed. To disable, set this option to false.

        maxValue: false
            The maximum value allowed. To disable, set this option to false.

        smallIncrement: 1
            The small increment that is used if the "ctrl" key is pressed.

        increment: 5
            The default increment that is used if no key modifiers are pressed.

        largeIncrement: 10
            The large increment that is used if the "shift" key is pressed, or when "page up" or "page down" are pressed.

        calc: null
            Function that is called to calculate what the next value is when incrementing/decrementing the value of the input.
            Providing this function will override the default functionality which is to add/subtract the increment amount.
            Function definition: Number function(value, type, direction)
                where value is the current input value.
                where type is a number: 1 = normal increment, 2 = small increment, and 3 = large increment.
                where direction is a number: 1 = up, 2 = down.
                returns the new value of the input.
            For example, to have the widget multiply it's value by 2 on a small increment, mulitply by 4 on a normal increment,
            and mulitply by 8 on a large increment, you would do something like this:
                $('#inputid').numeric({
                    emptyValue: false,
                    minValue: 0,
                    calc: function(val, type, dir)
                    {
                        if (val < 1)
                            return dir == 1 ? 1 : 0;

                        var mult = type == 2 ? 2 : (type == 3 ? 8 : 4);
                        return dir == 2 ? (val / mult) : (val * mult);
                    }
                });

        format: null
            The format information to use to format the number in the input. The "format" property is the format string to use
            (see below for details). The "decimalChar" property is the decimal character to use if the format string specifies
            to include decimal places. The "thousandsChar" is the thousands separator character to use, if the format string
            specifies to use a thousands separator.
    
            If the value of this setting is a string, then it is the same as:
                { format: "value", decimalChar: '.', thousandsChar: ',' }
            where "value" is the value of the string. 
*/

        #region Fields
        private bool iHasSettings = false;
        #endregion

        #region Configuration Properties
        public string FormID { get; private set; }

        // Options
        public int? MaxValue { get; private set; }
        public int? MinValue { get; private set; }
        public int? Increment { get; private set; }
        #endregion

        #region Configuration Methods
        /// <summary>
        /// Sets HTML ID of the parent form in order to make it possible to have NumericUpDownControl with same ID on a single page
        /// (e.g. in dialog boxes).
        /// </summary>
        public NumericUpDownControl SetFormID(string pFormID)
        {
            this.FormID = pFormID;
            return this;
        }

        /// <summary>
        /// Sets the maximum value allowed.
        /// </summary>
        public NumericUpDownControl SetMaxValue(int pMaxValue)
        {
            this.MaxValue = pMaxValue;
            this.iHasSettings = true;
            return this;
        }

        /// <summary>
        /// Sets the minimum value allowed.
        /// </summary>
        public NumericUpDownControl SetMinValue(int pMinValue)
        {
            this.MinValue = pMinValue;
            this.iHasSettings = true;
            return this;
        }

        /// <summary>
        /// Set sthe default increment that is used if no key modifiers are pressed
        /// </summary>
        public NumericUpDownControl SetIncrement(int pIncrement)
        {
            this.Increment = pIncrement;
            this.iHasSettings = true;
            return this;
        }
        #endregion

        #region Render Method
        public void RenderScript(
            StringBuilder pStringBuilder,
            string pHtmlName)
        {
            // Render javascript for jQuery numeric widget
         
            string vHtmlID = TagBuilder.CreateSanitizedId(pHtmlName, "");  //***


            // No need to wrap JavaScript code into a $(document).ready event handler
            //            pStringBuilder.Append(@"
            //<script type='text/javascript'>
            //    $('#");
            //            pStringBuilder.Append(vHtmlID);

            //            if (!string.IsNullOrEmpty(this.FormID))
            //            {
            //                pStringBuilder.Append("', '#");
            //                pStringBuilder.Append(this.FormID);
            //            }

            //            pStringBuilder.Append(@"')(");

            //            if (this.iHasSettings)
            //            {
            //                pStringBuilder.Append("{");
            //                bool vIsNext = false;

            //                if (this.MaxValue != null)
            //                {
            //                    if (vIsNext)
            //                    {
            //                        pStringBuilder.Append(",");
            //                    }
            //                    vIsNext = true;

            //                    pStringBuilder.Append("maxValue: ");
            //                    pStringBuilder.Append(this.MaxValue);
            //                }

            //                if (this.MinValue != null)
            //                {
            //                    if (vIsNext)
            //                    {
            //                        pStringBuilder.Append(",");
            //                    }
            //                    vIsNext = true;

            //                    pStringBuilder.Append("minValue: ");
            //                    pStringBuilder.Append(this.MinValue);
            //                }

            //                if (this.Increment != null)
            //                {
            //                    if (vIsNext)
            //                    {
            //                        pStringBuilder.Append(",");
            //                    }
            //                    vIsNext = true;

            //                    pStringBuilder.Append("increment: ");
            //                    pStringBuilder.Append(this.Increment);
            //                }

            //                pStringBuilder.Append("}");
            //            }

            //            pStringBuilder.Append(@");
            //</script>
            //");
        }
        #endregion
    }
}
