using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModels.General;
using X.PagedList;

namespace Services
{
    public class ApiServiceGeneric<T> : object
    {

        #region public ApiPagingResponseViewModel<T> ToPaginate(this IPagedList<T> inputValue)
        public ApiPagingResponseViewModel<T> ToPaginate(IPagedList<T> inputValue)
        {
            return new ApiPagingResponseViewModel<T>(
                inputValue.Count,
                inputValue.FirstItemOnPage,
                inputValue.HasNextPage,
                inputValue.HasPreviousPage,
                inputValue.IsFirstPage,
                inputValue.IsLastPage,
                inputValue.LastItemOnPage,
                inputValue.PageCount,
                inputValue.PageNumber,
                inputValue.PageSize,
                inputValue.TotalItemCount,
                inputValue.ToList());
        }
        #endregion /public ApiPagingResponseViewModel<T> ToPaginate(this IPagedList<T> inputValue)

        #region public static Expression<Func<T, bool>> EqualPredicate(string columnName, object searchValue)
        public static Expression<Func<T, bool>> EqualPredicate(string columnName, object searchValue)
        {
            var xType = typeof(T);
            var x = Expression.Parameter(xType, "x");
            var column = xType.GetProperties().FirstOrDefault(p => p.Name == columnName);

            var body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, columnName),
                    Expression.Constant(searchValue));

            return Expression.Lambda<Func<T, bool>>(body, x);
        }
        #endregion /public static Expression<Func<T, bool>> EqualPredicate(string columnName, object searchValue)

       
    }
}
