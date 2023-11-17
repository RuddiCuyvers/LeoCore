using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Grid edit column for edittype:'select'.
    /// Renders the column as a dropdownlist in edit mode.
    /// </summary>
    public class DropDownListColumn : BaseEditColumn<DropDownListColumn>, IGridEditColumn
    {
        #region Fields
        private bool iMultiple;
        private int iSize;
        private string iUrl;
        private string iValueLabelString;
        private IDictionary<string, string> iValueLabelDictionary;
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the size of the DropDownList.
        /// </summary>
        /// <param name="pSize">the size.</param>
        /// <returns></returns>
        public DropDownListColumn SetSize(int pSize)
        {
            this.iSize = pSize;
            return this;
        }

        /// <summary>
        /// Sets whether multiple selections are possible or not.
        /// </summary>
        /// <param name="pMultiple">if set to <c>true</c> multiple select list.</param>
        /// <returns></returns>
        public DropDownListColumn SetMultiple(bool pMultiple)
        {
            this.iMultiple = pMultiple;
            return this;
        }

        /// <summary>
        /// Sets the url for fetching the dropdownlist elements through an ajax call .
        /// The data returned from the ajax request should be a valid HTML select element. 
        /// </summary>
        /// <param name="pUrl">the url.</param>
        /// <returns></returns>
        public DropDownListColumn SetUrl(string pUrl)
        {
            this.iUrl = pUrl;
            return this;
        }

        /// <summary>
        /// Sets the values/labels formatted string used as the dropdownlist elements.
        /// String must be correctly formatted with sets of value:label pairs, for example:
        /// “FE:FedEx; IN:InTime; TN:TNT”
        /// </summary>
        /// <param name="pValueLabelString">The value label string.</param>
        /// <returns></returns>
        public DropDownListColumn SetValues(string pValueLabelString)
        {
            this.iValueLabelString = pValueLabelString;
            return this;
        }

        /// <summary>
        /// Sets the values/labels dictionary used as the dropdownlist elements.
        /// </summary>
        /// <param name="pValueLabelDictionary">The value label dictionary.</param>
        /// <returns></returns>
        public DropDownListColumn SetValues(IDictionary<string, string> pValueLabelDictionary)
        {
            this.iValueLabelDictionary = pValueLabelDictionary;
            return this;
        }
        #endregion

        #region IGridEditColumn implementation
        public override string Render()
        {
            var vBuilder = new StringBuilder();
            // Add formatter:'select' otherwise only the text of the dropdown is returned at submission, no the value...
            // vBuilder.Append(", edittype: \"select\"");
            vBuilder.Append(", formatter:'select', edittype: \"select\"");

            string vDataEvents = this.RenderDataEvents();
            bool vIsFirstOption = true;

            if (this.iMultiple
                || this.iSize != 0
                || (this.iValueLabelDictionary != null && this.iValueLabelDictionary.Count != 0)
                || !string.IsNullOrEmpty(this.iValueLabelString)
                || !string.IsNullOrEmpty(this.iUrl)
                || !string.IsNullOrEmpty(vDataEvents))
            {
                vBuilder.Append(", editoptions: { ");

                if (!string.IsNullOrEmpty(vDataEvents))
                {
                    vIsFirstOption = false;
                    vBuilder.Append(vDataEvents);
                }

                if (this.iMultiple)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("multiple: true");
                }

                if (this.iSize != 0)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("size: ");
                    vBuilder.Append(this.iSize);
                }

                // Either Url (remote data), ValueLabelString or ValueLabelDictionary (local data) is specified
                if (!string.IsNullOrEmpty(this.iUrl))
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("dataUrl : \"");
                    vBuilder.Append(this.iUrl);
                    vBuilder.Append("\"");
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.iValueLabelString))
                    {
                        if (!vIsFirstOption)
                        {
                            vBuilder.Append(", ");
                        }
                        else
                        {
                            vIsFirstOption = false;
                        }
                        vBuilder.Append("value : \"");
                        vBuilder.Append(this.iValueLabelString);
                        vBuilder.Append("\"");
                    }
                    else
                    {
                        if (this.iValueLabelDictionary != null && this.iValueLabelDictionary.Count != 0)
                        {
                            if (!vIsFirstOption)
                            {
                                vBuilder.Append(", ");
                            }
                            else
                            {
                                vIsFirstOption = false;
                            }
                            // Construct a string with sets of value:label pairs, example:
                            // value: “FE:FedEx; IN:InTime; TN:TNT”
                            vBuilder.Append("value : \"");
                            bool vIsFirstValue = true;
                            foreach (KeyValuePair<string, string> vKeyValuePair in this.iValueLabelDictionary)
                            {
                                if (!vIsFirstValue)
                                {
                                    // Don't add space behind ';' char, otherwise causes empty spaces in value
                                    vBuilder.Append(";");
                                }
                                else
                                {
                                    vIsFirstValue = false;
                                }
                                vBuilder.Append(vKeyValuePair.Key); // the value
                                vBuilder.Append(":");
                                vBuilder.Append(vKeyValuePair.Value); // the label
                            }
                            vBuilder.Append("\"");
                        }
                        else
                        {
                            // TODO
                            // Value as an object, example:
                            // value:{1:'One',2:'Two'}
                        }
                    }

                }

                vBuilder.Append(" }"); // end editoptions               
            }
            return vBuilder.ToString();
        }
        #endregion
    }
}
