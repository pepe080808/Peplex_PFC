using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peplex_PFC.UI.Shared
{
    // Use LINQ to sorter a List by multiple and variable field
    // http://www.codeproject.com/Articles/280952/Multiple-Column-Sorting-by-Field-Names-Using-Linq
    public static class ClassOrderBy
    {
        public static IEnumerable<T> MultipleSort<T>(this IEnumerable<T> data, List<Tuple<string, string>> sortExpressions)
        {
            // No sorting needed
            if ((sortExpressions == null) || (sortExpressions.Count <= 0))
                return data;

            // Let us sort it
            var query = from item in data select item;
            IOrderedEnumerable<T> orderedQuery = null;

            for (int i = 0; i < sortExpressions.Count; i++)
            {
                // We need to keep the loop index, not sure why it is altered by the Linq.
                var index = i;
                Func<T, object> expression = item => item.GetType().GetProperty(sortExpressions[index].Item1).GetValue(item, null);

                if (sortExpressions[index].Item2 == "asc")
                    orderedQuery = (index == 0) ? query.OrderBy(expression) : orderedQuery.ThenBy(expression);
                else
                    orderedQuery = (index == 0) ? query.OrderByDescending(expression) : orderedQuery.ThenByDescending(expression);
            }

            query = orderedQuery;

            return query;
        }
    }
}
