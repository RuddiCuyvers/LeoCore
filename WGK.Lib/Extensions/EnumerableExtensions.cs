using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WGK.Lib.Exceptions;

namespace WGK.Lib.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// ForEach pattern: performs an action on each of the items in a sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pSource"></param>
        /// <param name="pAction"></param>
        /// <example>
        /// var lBooks = from ... where ... select ... ;
        /// lBooks.ForEach(pBook => Console.WriteLine(pBook.Title));
        /// </example>
        public static void ForEach<T>(this IEnumerable<T> pSource, Action<T> pAction)
        {
            foreach (var vItem in pSource)
            {
                pAction(vItem);
            }
        }

        /// <summary>
        /// Finds the item with the maximum value in a sequence
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="pSource"></param>
        /// <param name="pSelector">Function used to compare sequence items</param>
        /// <returns></returns>
        /// <example>
        /// var lBooks = from ... where ... select ... ;
        /// var lMaxBook = lBooks.MaxElement(pBook => pBook.PageCount);
        /// </example>
        public static TElement MaxElement<TElement, TData>(
          this IEnumerable<TElement> pSource,
          Func<TElement, TData> pSelector)
          where TData : IComparable<TData>
        {
            if (pSource == null)
            {
                throw new ParameterMissingException("pSource");
            }
            if (pSelector == null)
            {
                throw new ParameterMissingException("pSelector");
            }

            Boolean vFirstElement = true;
            TElement vResult = default(TElement);
            TData vMaxValue = default(TData);
            foreach (TElement vElement in pSource)
            {
                var vCandidate = pSelector(vElement);
                if (vFirstElement || (vCandidate.CompareTo(vMaxValue) > 0))
                {
                    vFirstElement = false;
                    vMaxValue = vCandidate;
                    vResult = vElement;
                }
            }
            return vResult;
        }

        /// <summary>
        /// Checks if a sequence is empty. This method is optimized for collections that implement the Count property.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public static bool IsEmpty<TElement>(this IEnumerable<TElement> pSource)
        {
            if (pSource == null)
            {
                throw new ParameterMissingException("pSource");
            }

            var vGenericCollection = pSource as ICollection<TElement>;
            if (vGenericCollection != null)
            {
                return vGenericCollection.Count == 0;
            }

            var vNonGenericCollection = pSource as ICollection;
            if (vNonGenericCollection != null)
            {
                return vNonGenericCollection.Count == 0;
            }

            return !pSource.Any();
        }

        public interface ISoftDeletable
        {
            bool SoftDeleted { get; }
        }
    }
}
