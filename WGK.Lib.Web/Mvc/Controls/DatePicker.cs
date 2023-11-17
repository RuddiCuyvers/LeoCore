// TODO Review this code

//using System.Collections.Generic;
//using System.Text;

//namespace WGK.Lib.Web.Mvc.Controls
//{
//    public class DatePicker
//    {
//        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

//        /// <summary>
//        /// Creates a DatePicker control behaviour
//        /// </summary>
//        /// <param name="onSelectCallBack">onSelect js event handler</param>
//        /// <param name="onBeforeShowCallBack">onBeforeShow js event handler</param>
//        /// <param name="onCloseCallBack">onClose js event handler</param>
//        /// <param name="addtionalParameters">additional unmapped parameters</param>
//        /// <param name="isRTL">control alignment, defaults to no</param>
//        /// <param name="showOn">when should the date picker be shown: focos (control focus) or button (show a dedicated button)</param>
//        /// <param name="dateFormat">the format of the date</param>
//        /// <param name="buttonText">if showOn=button, sets the text of the dedicated button</param>
//        /// <param name="buttonImage">if showOn=button, sets the background image of the dedicated button</param>
//        /// <param name="buttonImageOnly">if showOn=button, sets the dedicated button to be image-only</param>
//        /// <param name="maxDate">sets the max date</param>
//        /// <param name="minDate">sets the min date</param>
//        public DatePicker(string onSelectCallBack = null,
//            string onBeforeShowCallBack = null,
//            string onCloseCallBack = null,
//            string addtionalParameters = null,
//            bool isRTL = false,
//            string showOn = "focus",
//            string dateFormat = null,
//            string buttonText = null,
//            string buttonImage = null,
//            bool buttonImageOnly = false,
//            string maxDate = null,
//            string minDate = null)
//        {
//            if (onSelectCallBack != null)
//            {
//                this.OnSelectCallBack = onSelectCallBack;
//            }
//            if (onBeforeShowCallBack != null)
//            {
//                this.OnBeforeShowCallBack = onBeforeShowCallBack;
//            }
//            if (onCloseCallBack != null)
//            {
//                this.OnCloseCallBack = onCloseCallBack;
//            }
//            if (addtionalParameters != null)
//            {
//                this.AdditionalParameters = addtionalParameters;
//            }
//            this.IsRTL = isRTL;
//            if (showOn != null)
//            {
//                this.ShowOn = showOn;
//            }
//            if (dateFormat != null)
//            {
//                this.DateFormat = dateFormat;
//            }
//            if (buttonImage != null)
//            {
//                this.ButtonImage = buttonImage;
//            }
//            if (buttonText != null)
//            {
//                this.ButtonText = buttonText;
//            }
//            this.ButtonImageOnly = buttonImageOnly;
//            if (minDate != null)
//            {
//                this.MinDate = minDate;
//            }
//            if (maxDate != null)
//            {
//                this.MaxDate = maxDate;
//            }
//        }

//        /// <summary>
//        /// Sets an onselect js callback with the following prototype: fn(dateText,inst)
//        /// </summary>
//        public string OnSelectCallBack
//        {
//            get { return this._attributes["onSelect"]; }
//            set { this._attributes["onSelect"] = value; }
//        }

//        /// <summary>
//        /// Sets an onselect js callback with the following prototype: fn(dateText,inst)
//        /// </summary>
//        public string OnBeforeShowCallBack
//        {
//            get { return this._attributes["beforeShow"]; }
//            set { this._attributes["beforeShow"] = value; }
//        }

//        /// <summary>
//        /// Sets an onselect js callback with the following prototype: fn(dateText,inst)
//        /// </summary>
//        public string OnCloseCallBack
//        {
//            get { return this._attributes["onClose"]; }
//            set { this._attributes["onClose"] = value; }
//        }

//        /// <summary>
//        /// Can be used to add additional parameters not conifgurable using the predefined properties
//        /// Format: paramName:\"paramValue\",paramName:\"paramValue\"
//        /// </summary>
//        public string AdditionalParameters { get; set; }

//        /// <summary>
//        /// Define when the calendar is visible:
//        /// focus - When the field recieve focus
//        /// button - A dedicated button
//        /// </summary>
//        public string ShowOn
//        {
//            get { return this.getString("showOn"); }
//            set { this.setString("showOn", value); }
//        }

//        /// <summary>
//        /// The date format, i.e: 'yy-mm-dd' 
//        /// </summary>
//        public string DateFormat
//        {
//            get { return this.getString("dateFormat"); }
//            set { this.setString("dateFormat", value); }
//        }

//        /// <summary>
//        /// Whether or not the text alignment should be rtl
//        /// </summary>
//        public bool IsRTL
//        {
//            get
//            {
//                if (this._attributes.ContainsKey("isRTL"))
//                {
//                    return bool.Parse(this._attributes["isRTL"]);
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            set { this._attributes["isRTL"] = value.ToString().ToLower(); }
//        }

//        /// <summary>
//        /// The button's text caption
//        /// Setting this property will automatically set {showOn=button
//        /// </summary>
//        public string ButtonText
//        {
//            get { return this.getString("buttonText"); }
//            set
//            {
//                this.setString("buttonText", value);
//                this.ShowOn = "button";
//            }
//        }

//        /// <summary>
//        /// The url to the image for the button
//        /// Setting this property will automatically set {showOn=button
//        /// </summary>
//        public string ButtonImage
//        {
//            get { return this.getString("buttonImage"); }
//            set
//            {
//                this.setString("buttonImage", value);
//                this.ShowOn = "button";
//            }
//        }

//        /// <summary>
//        /// Whether the button should be image only
//        /// Setting this property will automatically set {showOn=button
//        /// </summary>
//        public bool? ButtonImageOnly
//        {
//            get
//            {
//                if (this._attributes.ContainsKey("buttonImageOnly"))
//                {
//                    return bool.Parse(this._attributes["buttonImageOnly"]);
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            set { this._attributes["buttonImageOnly"] = value.ToString().ToLower(); }
//        }

//        /// <summary>
//        /// The maximum date compared to today, i.e: +1m +1w = month and week max, or a static date
//        /// </summary>
//        public string MaxDate
//        {
//            get { return this.getString("maxDate"); }
//            set { this.setString("maxDate", value); }
//        }

//        /// <summary>
//        /// The minimum date compared to today, i.e: -1m -1w = month and week max, or a static date
//        /// </summary>
//        public string MinDate
//        {
//            get { return this.getString("minDate"); }
//            set { this.setString("minDate", value); }
//        }

//        /// <summary>
//        /// Renders the DatePicker extension for the specified control
//        /// </summary>
//        /// <param name="id">The control's html-id</param>
//        /// <returns></returns>
//        public string Render(string id)
//        {
//            // No need to wrap code into a $(document).ready event handler
//            var sb = new StringBuilder();

//            //.datepicker({ buttonText: 'Choose' });
//            //sb.AppendFormat("$(#{0}).datepicker({", id);

//            foreach (string key in this._attributes.Keys)
//            {
//                sb.AppendFormat(",{0}:{1}", key, this._attributes[key]);
//            }

//            if (!string.IsNullOrEmpty(this.AdditionalParameters))
//            {
//                sb.AppendFormat(",{0}", this.AdditionalParameters);
//            }

//            if (sb.Length > 0)
//            {
//                sb.Remove(0, 1);
//            }

//            sb.Append("});\r\n");

//            sb.Insert(0, "$(\"#" + id + "\").datepicker({");

//            sb.Insert(0, "<script type='text/javascript'>\r\n");
//            sb.AppendLine("</script>");
//            return sb.ToString();
//        }

//        private string getString(string key)
//        {
//            if (!this._attributes.ContainsKey(key))
//            {
//                return null;
//            }
//            string temp = this._attributes[key];
//            return temp.Substring(1, temp.Length - 2);
//        }

//        private void setString(string key, string value)
//        {
//            if (string.IsNullOrEmpty(value))
//            {
//                this._attributes[key] = null;
//            }
//            else
//            {
//                this._attributes[key] = "\"" + value + "\"";
//            }
//        }
//    }
//}