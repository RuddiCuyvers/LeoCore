using System;
using System.Collections.Generic;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Defines GridCellTypes for local data sorting
    /// </summary>
    public enum HtmlEventType
    {
        Blur,
        Change,
        Click,
        Dblclick,
        Error,
        Focus,
        Focusin,
        Focusout,
        Keydown,
        Keypress,
        Keyup,
        Load,
        Mousedown,
        Mouseenter,
        Mouseleave,
        Mousemove,
        Mouseout,
        Mouseover,
        Mouseup,
        Ready,
        Resize,
        Scroll,
        Select,
        Submit,
        Unload
    }

    public abstract class BaseEditColumn<TEditColumn> : IGridEditColumn where TEditColumn : class, IGridEditColumn
    {
        #region Constants
        private static readonly Dictionary<HtmlEventType, string> cHtmlEventTypes = new Dictionary<HtmlEventType, string>
        {
            {HtmlEventType.Blur, "blur"}, 
            {HtmlEventType.Change, "change"}, 
            {HtmlEventType.Click, "click"}, 
            {HtmlEventType.Dblclick, "dblclick"}, 
            {HtmlEventType.Error, "error"}, 
            {HtmlEventType.Focus, "focus"}, 
            {HtmlEventType.Focusin, "focusin"}, 
            {HtmlEventType.Focusout, "focusout"}, 
            {HtmlEventType.Keydown, "keydown"}, 
            {HtmlEventType.Keypress, "keypress"}, 
            {HtmlEventType.Keyup, "keyup"}, 
            {HtmlEventType.Load, "load"}, 
            {HtmlEventType.Mousedown, "mousedown"}, 
            {HtmlEventType.Mouseenter, "mouseenter"}, 
            {HtmlEventType.Mouseleave, "mouseleave"}, 
            {HtmlEventType.Mousemove, "mousemove"}, 
            {HtmlEventType.Mouseout, "mouseout"}, 
            {HtmlEventType.Mouseover, "mouseover"}, 
            {HtmlEventType.Mouseup, "mouseup"}, 
            {HtmlEventType.Ready, "ready"}, 
            {HtmlEventType.Resize, "resize"}, 
            {HtmlEventType.Scroll, "scroll"}, 
            {HtmlEventType.Select, "select"}, 
            {HtmlEventType.Submit, "submit"}, 
            {HtmlEventType.Unload, "unload"} 
        };
        #endregion
    
        #region Fields
        // Dictionary for storing list of events to apply to the data element
        private IDictionary<string, Tuple<string, string>> iDataEventsDictionary;
        #endregion

        #region Public Methods
        /// <summary>
        /// Binds a javascript event handler to the data element for this column
        /// </summary>
        /// <param name="pEventType">HTML-element event type, e.g. "click"</param>
        /// <param name="pFunction">Event handler function or function name, e.g. "function(e) { console.log(e.data.i); }"</param>
        /// <param name="pData">Optional json data that is passed on to the bind method, e.g. "{ i: 7 }" </param>
        /// <returns></returns>
        public TEditColumn AddDataEvent(HtmlEventType pEventType, string pFunction, string pData = null)
        {
            return this.AddDataEvent(cHtmlEventTypes[pEventType], pFunction, pData);
        }

        /// <summary>
        /// Binds a javascript event handler to the data element for this column
        /// </summary>
        /// <param name="pEventName">HTML-element event name, e.g. "click"</param>
        /// <param name="pFunction">Event handler function or function name, e.g. "function(e) { console.log(e.data.i); }"</param>
        /// <param name="pData">Optional json data that is passed on to the bind method, e.g. "{ i: 7 }" </param>
        /// <returns></returns>
        public TEditColumn AddDataEvent(string pEventName, string pFunction, string pData = null)
        {
            if (string.IsNullOrEmpty(pEventName) || string.IsNullOrEmpty(pFunction))
            {
                // Ignore
                return this as TEditColumn;
            }

            if (this.iDataEventsDictionary == null)
            {
                this.iDataEventsDictionary = new Dictionary<string, Tuple<string, string>>();
            }

            this.iDataEventsDictionary.Add(pEventName, new Tuple<string, string>(pFunction, pData));
            return this as TEditColumn;
        }
        #endregion

        #region IGridEditColumn implementation
        /// <summary>
        /// Derived class must implement this method to render EditType and EditOptions for its specific column type.
        /// </summary>
        /// <returns></returns>
        public abstract string Render();

        /// <summary>
        /// Gets/sets the GridColumnModel instance that this edit column applies to
        /// </summary>
        public GridColumnModel Column { get; set; }
        #endregion
        
        #region Protected methods
        /// <summary>
        /// Renders the dataEvents for the edit column
        /// Derived class must call this method from its Render method.
        /// </summary>
        /// <returns></returns>
        protected string RenderDataEvents()
        {
            if (this.iDataEventsDictionary == null || (this.iDataEventsDictionary.Count == 0))
            {
                return null;
            }

            var vBuilder = new StringBuilder();
            vBuilder.Append("dataEvents: [");
            bool vIsFirst = true;

            foreach (string vEventName in this.iDataEventsDictionary.Keys)
            {
                if (vIsFirst)
                {
                    vIsFirst = false;
                }
                else
                {
                    vBuilder.Append(", ");
                }

                // Event type
                vBuilder.Append("{ type: '");
                vBuilder.Append(vEventName);
                vBuilder.Append("'"); 

                // Event data
                if (!string.IsNullOrEmpty(this.iDataEventsDictionary[vEventName].Item2))
                {
                    vBuilder.Append(", data: ");
                    vBuilder.Append(vEventName);                    
                }

                // Event handler function
                vBuilder.Append(", fn: ");
                vBuilder.Append(this.iDataEventsDictionary[vEventName].Item1);

                vBuilder.Append(" }");
            }

            vBuilder.Append("]");
            return vBuilder.ToString();
        }
        #endregion

    }
}