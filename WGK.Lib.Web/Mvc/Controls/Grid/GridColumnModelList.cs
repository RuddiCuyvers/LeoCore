using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using WGK.Lib.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// This class allows for fluent column adding
    /// </summary>
    /// <typeparam name="T">Collection item model type</typeparam>
    public class GridColumnModelList<T> where T : class
    {
        internal List<GridColumnModel> iItems { get; set; }

        /// <summary>
        /// Gets the model metadata associated with model type T.
        /// </summary>
        /// <value>The model metadata.</value>
        public ModelMetadata ModelMetadata
        {
            get
            {
                if (this.iModelMetadata == null)
                {
                    // Cache ModelMetadata so we don't have to fetch it for every column
                  //  this.iModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(T));
                }
                return this.iModelMetadata;
            }
        }
        private ModelMetadata iModelMetadata = null;


        public GridColumnModelList()
        {
            this.iItems = new List<GridColumnModel>();
        }

        /// <summary>
        /// Gets the meta data for a property on the model type T.
        /// </summary>
        /// <param name="pPropertyName">Name of the p property.</param>
        /// <returns></returns>
        private ModelMetadata GetPropertyMetaData(string pPropertyName)
        {
            return this.ModelMetadata.Properties.Where(p => p.PropertyName == pPropertyName).FirstOrDefault();
        }

        /// <summary>
        /// Adds a column for the specified property expression
        /// </summary>
        /// <param name="pExpression">The property expression.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(Expression<Func<T, object>> pExpression)
        {
            return this.Add(pExpression.MemberNameWithoutInstance());
        }

        /// <summary>
        /// Adds a column for the specified property name.
        /// </summary>
        /// <param name="pPropertyName">Name of the property.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(string pPropertyName)
        {
            GridColumnModel vColumn;
            var vPropertyMetaData = this.GetPropertyMetaData(pPropertyName);
            if (vPropertyMetaData != null && vPropertyMetaData.DisplayName != null)
            {
                vColumn = new GridColumnModel(pPropertyName, vPropertyMetaData.DisplayName);
            }
            else
            {
                vColumn = new GridColumnModel(pPropertyName);
            }
            this.iItems.Add(vColumn);
            return vColumn;
        }

        /// <summary>
        /// Adds a column for the specified property expression with display name.
        /// </summary>
        /// <param name="pExpression">The property expression.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(Expression<Func<T, object>> pExpression, string pDisplayName)
        {
            return this.Add(pExpression.MemberNameWithoutInstance(), pDisplayName);
        }

        /// <summary>
        /// Adds a column for the specified property name with display name.
        /// </summary>
        /// <param name="pPropertyName">The property name.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(string pPropertyName, string pDisplayName)
        {
            if (pDisplayName != null)
            {
                GridColumnModel vColumn = new GridColumnModel(pPropertyName, pDisplayName);
                this.iItems.Add(vColumn);
                return vColumn;
            }
            else
            {
                return this.Add(pPropertyName);
            }
        }

        /// <summary>
        /// Adds a column for the specified property expression with display name and width.
        /// </summary>
        /// <param name="pExpression">The property expression.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <param name="pWidth">Width of the column.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(Expression<Func<T, object>> pExpression, string pDisplayName, string pWidth)
        {
            return this.Add(pExpression.MemberNameWithoutInstance(), pDisplayName, pWidth);
        }

        /// <summary>
        /// Adds a column for the specified property name with display name and width.
        /// </summary>
        /// <param name="pPropertyName">The property name.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <param name="pWidth">Width of the column.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(string pPropertyName, string pDisplayName, string pWidth)
        {
            string vCaption = null;
            if (pDisplayName != null)
            {
                vCaption = pDisplayName;
            }
            else
            {
                var vPropertyMetaData = this.GetPropertyMetaData(pPropertyName);
                if (vPropertyMetaData != null && vPropertyMetaData.DisplayName != null)
                {
                    vCaption = vPropertyMetaData.DisplayName;
                }
            }

            var vColumn = new GridColumnModel(pPropertyName, vCaption, pWidth); ;
            this.iItems.Add(vColumn);
            return vColumn;
        }

        /// <summary>
        /// Adds a column for the specified property expression with display name, width and alignment.
        /// </summary>
        /// <param name="pExpression">The property expression.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <param name="pWidth">Width of the column.</param>
        /// <param name="pAlign">The cell alignment.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(Expression<Func<T, object>> pExpression, string pDisplayName, string pWidth, Align pAlign)
        {
            return this.Add(pExpression.MemberNameWithoutInstance(), pDisplayName, pWidth, pAlign);
        }

        /// <summary>
        /// Adds a column for the specified property name with display name, width and alignment.
        /// </summary>
        /// <param name="pPropertyName">The property name.</param>
        /// <param name="pDisplayName">Display name for the column.</param>
        /// <param name="pWidth">Width of the column.</param>
        /// <param name="pAlign">The cell alignment.</param>
        /// <returns>The column</returns>
        public GridColumnModel Add(string pPropertyName, string pDisplayName, string pWidth, Align pAlign)
        {
            string vCaption = null;
            if (pDisplayName != null)
            {
                vCaption = pDisplayName;
            }
            else
            {
                var vPropertyMetaData = this.GetPropertyMetaData(pPropertyName);
                if (vPropertyMetaData != null && vPropertyMetaData.DisplayName != null)
                {
                    vCaption = vPropertyMetaData.DisplayName;
                }
            }

            var vColumn = new GridColumnModel(pPropertyName, vCaption, pWidth, pAlign);
            this.iItems.Add(vColumn);
            return vColumn;
        }
    }
}