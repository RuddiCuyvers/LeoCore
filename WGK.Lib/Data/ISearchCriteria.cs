using System.Linq;

namespace WGK.Lib.Data
{
    public interface ISearchCriteria<T>
    {
        IQueryable<T> Execute(IQueryable<T> pValues);
    }
}