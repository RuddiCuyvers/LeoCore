using System.Collections.Generic;

namespace WGK.Lib.UseCases
{
    public interface IAutoCompleteUseCase<TID> : IBaseUseCase
    {
        /// <summary>
        /// Gets/sets the criteria for filling an autocompleted list
        /// </summary>
        AutoCompleteCriteria Criteria { get; set; }

        /// <summary>
        /// Gets the autocompleted list
        /// </summary>
        IEnumerable<AutoCompleteResult<TID>> Result { get; }
    }

    public interface IAutoCompleteUseCase : IBaseUseCase
    {
        /// <summary>
        /// Gets/sets the criteria for filling an autocompleted list
        /// </summary>
        AutoCompleteCriteria Criteria { get; set; }

        /// <summary>
        /// Gets the autocompleted list
        /// </summary>
        IEnumerable<AutoCompleteResult> Result { get; }
    }
}