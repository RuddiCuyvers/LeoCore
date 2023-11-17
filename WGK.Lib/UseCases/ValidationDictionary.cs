using System.Collections.Generic;
using System.Text;

namespace WGK.Lib.UseCases
{
    /// <summary>
    /// Implementation of a ValidationDictionary for use outside ASP.NET MVC
    /// </summary>
    public class ValidationDictionary : IValidationDictionary
    {
        private Dictionary<string, List<string>> iErrorDictionary = new Dictionary<string, List<string>>();

        /// <summary>
        /// Adds a validation error to the dictionary.
        /// </summary>
        /// <param name="pKey">The key.</param>
        /// <param name="pErrorMessage">The error message.</param>
        public void AddError(string pKey, string pErrorMessage)
        {
            if (!this.iErrorDictionary.ContainsKey(pKey))
            {
                this.iErrorDictionary.Add(pKey, new List<string> {pErrorMessage});
            }
            else
            {
                this.iErrorDictionary[pKey].Add(pErrorMessage);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return  this.iErrorDictionary.Count == 0; }
        }

        /// <summary>
        /// Gets the validation summary message
        /// </summary>
        public string GetValidationSummary()
        {
            var vBuilder = new StringBuilder();

            foreach (var vKey in this.iErrorDictionary.Keys)
            {
                var vErrorMessages = this.iErrorDictionary[vKey];
                if (vErrorMessages != null)
                {
                    foreach (var vErrorMessage in vErrorMessages)
                    {
                        //vBuilder.Append(vKey);
                        //vBuilder.Append(" : ");
                        vBuilder.AppendLine(vErrorMessage);
                    }
                }
            }
            return vBuilder.ToString();
        }

        /// <summary>
        /// Merges validation messages into another ValidationDictionary
        /// </summary>
        public void MergeInto(IValidationDictionary pTarget)
        {
            foreach (var vKey in this.iErrorDictionary.Keys)
            {
                var vErrorMessages = this.iErrorDictionary[vKey];
                if (vErrorMessages != null)
                {
                    foreach (var vErrorMessage in vErrorMessages)
                    {
                        pTarget.AddError(vKey, vErrorMessage);
                    }
                }
            }
        }
    }
}